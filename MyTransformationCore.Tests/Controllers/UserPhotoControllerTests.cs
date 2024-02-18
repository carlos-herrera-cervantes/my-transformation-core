using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;

using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Web.Controllers;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Services.Aws;
using MyTransformationCore.Domain.Configs;

using Moq;
using MongoDB.Driver;

namespace MyTransformationCore.Tests.Controllers;

[ExcludeFromCodeCoverage]
[Collection("Controllers")]
public class UserPhotoControllerTests
{
    #region snippet_Properties

    private readonly Mock<IUserPhotoRepository> _mockUserPhotoRepository = new();

    private readonly Mock<IUserPhotoManager> _mockUserPhotoManager = new();

    private readonly Mock<IFormFile> _mockFormFile = new();

    private readonly Mock<IS3Service> _mockS3Service = new();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetMeAsyncShouldReturn200()
    {
        _mockUserPhotoRepository.
            Setup(upr => upr.GetAllAsync(It.IsAny<FilterDefinition<UserPhoto>>())).
            ReturnsAsync([]);

        var userPhotoController = new UserPhotoController(_mockUserPhotoRepository.Object, _mockUserPhotoManager.Object, _mockS3Service.Object);
        IActionResult httpResponse = await userPhotoController.GetMeAsync(userId: "65b7fe35a24e34af76de5884");

        _mockUserPhotoRepository.Verify(upr => upr.GetAllAsync(It.IsAny<FilterDefinition<UserPhoto>>()), Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 201 status code")]
    public async Task CreateMeAsyncShouldReturn201()
    {
        _mockUserPhotoManager.
            Setup(upm => upm.CreateAsync(It.IsAny<UserPhoto>())).
            Returns(Task.CompletedTask);
        _mockFormFile.Setup(ff => ff.FileName).Returns("profile.png");
        _mockS3Service
            .Setup(s3s => s3s.PutObjectAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
            .ReturnsAsync((string filename, string rootPath, Stream stream) =>
            {
                return $"{S3Config.DefaultEndpoint}/{S3Config.DefaultBucket}/{rootPath}/{filename}";
            });

        var userPhotoController = new UserPhotoController(_mockUserPhotoRepository.Object, _mockUserPhotoManager.Object, _mockS3Service.Object);
        IActionResult httpResponse = await userPhotoController.CreateMeAsync(
            userId: "65b7fe35a24e34af76de5884",
            userPhotoCreation: new UserPhotoCreation{
                Image = _mockFormFile.Object
            }
        );

        _mockUserPhotoManager.Verify(upm => upm.CreateAsync(It.IsAny<UserPhoto>()), Times.Once);
        _mockFormFile.Verify(ff => ff.FileName, Times.Exactly(2));
        _mockS3Service.Verify(s3s => s3s.PutObjectAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()), Times.Once);
        Assert.IsType<CreatedResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task DeleteMeAsyncShouldReturn404()
    {
        _mockUserPhotoRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserPhoto>>()))
            .ReturnsAsync(() => null);

        var userPhotoController = new UserPhotoController(_mockUserPhotoRepository.Object, _mockUserPhotoManager.Object, _mockS3Service.Object);
        IActionResult httpResponse = await userPhotoController.DeleteMeAsync(
            userId: "65b7fe35a24e34af76de5884",
            id: "65b9bbd7383c0c0db4796076"
        );

        _mockUserPhotoRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserPhoto>>()), Times.Once);
        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 204 status code")]
    public async Task DeleteMeAsyncShouldReturn204()
    {
        _mockUserPhotoRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserPhoto>>()))
            .ReturnsAsync(new UserPhoto());
        _mockUserPhotoManager
            .Setup(upm => upm.DeleteAsync(It.IsAny<FilterDefinition<UserPhoto>>()))
            .Returns(Task.CompletedTask);
        _mockS3Service.Setup(s3s => s3s.DeleteObjectAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        var userPhotoController = new UserPhotoController(_mockUserPhotoRepository.Object, _mockUserPhotoManager.Object, _mockS3Service.Object);
        IActionResult httpResponse = await userPhotoController.DeleteMeAsync(
            userId: "65b7fe35a24e34af76de5884",
            id: "65b9bbd7383c0c0db4796076"
        );

        _mockUserPhotoRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserPhoto>>()), Times.Once);
        _mockUserPhotoManager.Verify(upm => upm.DeleteAsync(It.IsAny<FilterDefinition<UserPhoto>>()), Times.Once);
        _mockS3Service.Verify(s3s => s3s.DeleteObjectAsync(It.IsAny<string>()), Times.Once);
        Assert.IsType<NoContentResult>(httpResponse);
    }

    #endregion
}
