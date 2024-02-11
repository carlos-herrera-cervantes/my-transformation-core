using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Xunit;

using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Web.Controllers;

using MongoDB.Driver;
using Moq;

namespace MyTransformationCore.Tests.Controllers;

[Collection("Controllers")]
[ExcludeFromCodeCoverage]
public class UserProgressControllerTests
{
    #region snippet_Properties

    private readonly Mock<IUserProgressRepository> _mockUserProgressRepository = new();

    private readonly Mock<IUserProgressManager> _mockUserProgressManager = new();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = nameof(GetAllMeAsyncShouldReturn200))]
    public async Task GetAllMeAsyncShouldReturn200()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAllAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync([]);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.GetAllMeAsync(userId: "65c6535e204bb3411f4a4ad9");

        Assert.IsType<OkObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAllAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    [Fact(DisplayName = nameof(GetMeAsyncShouldReturn404))]
    public async Task GetMeAsyncShouldReturn404()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(() => null);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.GetMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c654830cbb25d62e506e64"
        );

        Assert.IsType<NotFoundObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    [Fact(DisplayName = nameof(GetMeAsyncShouldReturn200))]
    public async Task GetMeAsyncShouldReturn200()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(new UserProgress());
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.GetMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c654830cbb25d62e506e64"
        );

        Assert.IsType<OkObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    [Fact(DisplayName = nameof(CreateMeAsyncShouldReturn201))]
    public async Task CreateMeAsyncShouldReturn201()
    {
        _mockUserProgressManager.Setup(upm => upm.CreateAsync(It.IsAny<UserProgress>())).Returns(Task.CompletedTask);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.CreateMeAsync(userId: "65c6535e204bb3411f4a4ad9", new UserProgressCreation());

        Assert.IsType<CreatedResult>(httpResponse);
        _mockUserProgressManager.Verify(upm => upm.CreateAsync(It.IsAny<UserProgress>()), Times.Once);
    }

    [Fact(DisplayName = nameof(UpdateMeAsyncShouldReturn404))]
    public async Task UpdateMeAsyncShouldReturn404()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(() => null);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.UpdateMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c6cd70e1b974723e4cb898",
            new UserProgressUpdate()
        );

        Assert.IsType<NotFoundObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    [Fact(DisplayName = nameof(UpdateMeAsyncShouldReturn200))]
    public async Task UpdateMeAsyncShouldReturn200()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(new UserProgress());
        _mockUserProgressManager
            .Setup(upm => upm.UpdateAsync(It.IsAny<FilterDefinition<UserProgress>>(), It.IsAny<UserProgress>()))
            .Returns(Task.CompletedTask);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.UpdateMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c6cd70e1b974723e4cb898",
            new UserProgressUpdate()
        );

        Assert.IsType<OkObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
        _mockUserProgressManager
            .Verify(upm => upm.UpdateAsync(It.IsAny<FilterDefinition<UserProgress>>(), It.IsAny<UserProgress>()), Times.Once);
    }

    [Fact(DisplayName = nameof(DeleteMeAsyncShouldReturn404))]
    public async Task DeleteMeAsyncShouldReturn404()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(() => null);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.DeleteMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c6cd70e1b974723e4cb898"
        );

        Assert.IsType<NotFoundObjectResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    [Fact(DisplayName = nameof(DeleteMeAsyncShouldReturn204))]
    public async Task DeleteMeAsyncShouldReturn204()
    {
        _mockUserProgressRepository
            .Setup(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .ReturnsAsync(new UserProgress());
        _mockUserProgressManager
            .Setup(upm => upm.DeleteAsync(It.IsAny<FilterDefinition<UserProgress>>()))
            .Returns(Task.CompletedTask);
        var userProgressController = new UserProgressController(_mockUserProgressRepository.Object, _mockUserProgressManager.Object);

        IActionResult httpResponse = await userProgressController.DeleteMeAsync(
            userId: "65c6535e204bb3411f4a4ad9",
            id: "65c6cd70e1b974723e4cb898"
        );

        Assert.IsType<NoContentResult>(httpResponse);
        _mockUserProgressRepository.Verify(upr => upr.GetAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
        _mockUserProgressManager.Verify(upm => upm.DeleteAsync(It.IsAny<FilterDefinition<UserProgress>>()), Times.Once);
    }

    #endregion
}
