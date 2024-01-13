using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserRepository))]
public class UserRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return an empty list")]
    public async Task GetAllAsyncShouldReturnEmptyList()
    {
        var userRepository = new UserRepository(_mongoClient);
        IEnumerable<User> users = await userRepository.GetAllAsync(Builders<User>.Filter.Eq(u => u.FirstName, "bad"));
        Assert.Empty(users);
    }

    [Fact(DisplayName = "Should return null")]
    public async Task GetAsyncShouldReturnNull()
    {
        var userRepository = new UserRepository(_mongoClient);
        User user = await userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.FirstName, "bad"));
        Assert.Null(user);
    }

    #endregion
}
