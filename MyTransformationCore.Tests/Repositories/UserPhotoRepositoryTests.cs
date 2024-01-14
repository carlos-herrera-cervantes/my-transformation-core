using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserPhotoRepository))]
public class UserPhotoRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return an empty list")]
    public async Task GetAllAsyncShouldReturnEmptyList()
    {
        var userPhotoRepository = new UserPhotoRepository(_mongoClient);
        IEnumerable<UserPhoto> photos = await userPhotoRepository.GetAllAsync(Builders<UserPhoto>.Filter.Eq(up => up.UserId, "65a2e2140db83d53defda2e3"));
        Assert.Empty(photos);
    }

    [Fact(DisplayName = "Should return null")]
    public async Task GetAsyncShouldReturnNull()
    {
        var userPhotoRepository = new UserPhotoRepository(_mongoClient);
        UserPhoto photo = await userPhotoRepository.GetAsync(Builders<UserPhoto>.Filter.Eq(up => up.UserId, "65a2e2140db83d53defda2e3"));
        Assert.Null(photo);
    }

    #endregion
}
