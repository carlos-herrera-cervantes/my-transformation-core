using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Web.Controllers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Domain.Models;

using Moq;
using MongoDB.Driver;

namespace MyTransformationCore.Tests.Controllers;

[ExcludeFromCodeCoverage]
[Collection("Controllers")]
public class UserControllerTests
{
    #region snippet_Properties

    private readonly Mock<IUserRepository> _mockUserRepository = new();

    private readonly Mock<IUserManager> _mockUserManager = new();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task GetMeAsyncShouldReturn404()
    {
        _mockUserRepository.
            Setup(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>())).
            ReturnsAsync(() => null);

        var userController = new UserController(_mockUserRepository.Object, _mockUserManager.Object);
        IActionResult httpResponse = await userController.GetMeAsync(id: "65adacf56ba4b4b7f4723618");

        _mockUserRepository.Verify(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>()), Times.Once);

        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetMeAsyncShouldReturn200()
    {
        _mockUserRepository.
            Setup(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>())).
            ReturnsAsync(new User());

        var userController = new UserController(_mockUserRepository.Object, _mockUserManager.Object);
        IActionResult httpResponse = await userController.GetMeAsync(id: "65adacf56ba4b4b7f4723618");

        _mockUserRepository.Verify(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>()), Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task UpdateMeAsyncShouldReturn404()
    {
        _mockUserRepository.
            Setup(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>())).
            ReturnsAsync(() => null);

        var userController = new UserController(_mockUserRepository.Object, _mockUserManager.Object);
        IActionResult httpResponse = await userController.UpdateMeAsync(id: "65adacf56ba4b4b7f4723618", new UserUpdate());

        _mockUserRepository.Verify(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>()), Times.Once);

        Assert.IsType<NotFoundObjectResult>(httpResponse);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task UpdateMeAsyncShouldReturn200()
    {
        _mockUserRepository.
            Setup(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>())).
            ReturnsAsync(new User());
        _mockUserManager.
            Setup(um => um.UpdateAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        var userController = new UserController(_mockUserRepository.Object, _mockUserManager.Object);
        IActionResult httpResponse = await userController.UpdateMeAsync(id: "65adacf56ba4b4b7f4723618", new UserUpdate());

        _mockUserRepository.Verify(ur => ur.GetAsync(It.IsAny<FilterDefinition<User>>()), Times.Once);
        _mockUserManager.Verify(um => um.UpdateAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<User>()), Times.Once);

        Assert.IsType<OkObjectResult>(httpResponse);
    }

    #endregion
}
