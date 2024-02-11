using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;

using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Web.Controllers;

using MongoDB.Driver;
using Moq;

namespace MyTransformationCore.Tests.Controllers;

[ExcludeFromCodeCoverage]
[Collection("Controllers")]
public class ExerciseControllerTests
{
    #region snippet_Properties

    private readonly Mock<IExerciseRepository> _mockExerciseRepository = new();

    private readonly Mock<IExerciseManager> _mockExerciseManager = new();

    private readonly Mock<IFormFile> _mockFormFile = new();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetAllAsyncShouldReturn200()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAllAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(new List<Exercise>());

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.GetAllAsync();

        _mockExerciseRepository.Verify(er => er.GetAllAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task GetAsyncShouldReturn404()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(() => null);

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.GetAsync(id: "65ac120d41738783ecbb25ac");

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetAsyncShouldReturn200()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(new Exercise());

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.GetAsync(id: "65ac120d41738783ecbb25ac");

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 201 status code")]
    public async Task CreateAsyncShouldReturn201()
    {
        _mockExerciseManager.Setup(em => em.CreateAsync(It.IsAny<Exercise>())).Returns(Task.CompletedTask);
        _mockFormFile.Setup(ff => ff.FileName).Returns("profile.png");

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.CreateAsync(new ExerciseCreation
        {
            Name = "Test Exercise",
            Image = _mockFormFile.Object,
            MuscleGroups = "test"
        });

        _mockExerciseManager.Verify(em => em.CreateAsync(It.IsAny<Exercise>()), Times.Once);

        Assert.IsType<CreatedResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task UpdateAsyncShouldReturn404()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(() => null);

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.UpdateAsync(id: "65ac120d41738783ecbb25ac", new ExerciseUpdate());

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task UpdateAsyncShouldReturn200()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(() => new Exercise());
        _mockExerciseManager
            .Setup(em => em.UpdateAsync(It.IsAny<FilterDefinition<Exercise>>(), It.IsAny<Exercise>()))
            .Returns(Task.CompletedTask);
        _mockFormFile.Setup(ff => ff.FileName).Returns("profile.png");

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.UpdateAsync(id: "65ac120d41738783ecbb25ac", new ExerciseUpdate
        {
            Image = _mockFormFile.Object
        });

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);
        _mockExerciseManager
            .Verify(em => em.UpdateAsync(It.IsAny<FilterDefinition<Exercise>>(), It.IsAny<Exercise>()), Times.Once);
        _mockFormFile.Verify(ff => ff.FileName, Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task DeleteAsyncShouldReturn404()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(() => null);

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.DeleteAsync(id: "65ac120d41738783ecbb25ac");

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 204 status code")]
    public async Task DeleteAsyncShouldReturn204()
    {
        _mockExerciseRepository
            .Setup(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .ReturnsAsync(new Exercise());
        _mockExerciseManager
            .Setup(em => em.DeleteAsync(It.IsAny<FilterDefinition<Exercise>>()))
            .Returns(Task.CompletedTask);

        var exerciseController = new ExerciseController(_mockExerciseRepository.Object, _mockExerciseManager.Object);
        IActionResult httpResponse = await exerciseController.DeleteAsync(id: "65ac120d41738783ecbb25ac");

        _mockExerciseRepository.Verify(er => er.GetAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);
        _mockExerciseManager.Verify(em => em.DeleteAsync(It.IsAny<FilterDefinition<Exercise>>()), Times.Once);

        Assert.IsType<NoContentResult>(httpResponse);
    }

    #endregion
}
