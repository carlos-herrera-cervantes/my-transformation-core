using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Managers;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserPhotoManager))]
public class UserPhotoManagerTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should execute all the processes that make up the CRUD")]
    public async Task Crud()
    {
        var userPhotoManager = new UserPhotoManager(_mongoClient);

        await userPhotoManager.CreateAsync(new UserPhoto
        {
            UserId = "65a2da18b0ee7a748273d9b4",
            Path = "/65a2da18b0ee7a748273d9b4/2024-01-01.png",
            Moment = DateTime.UtcNow
        });
        await userPhotoManager.DeleteAsync(Builders<UserPhoto>.Filter.Eq(up => up.UserId, "65a2da18b0ee7a748273d9b4"));
    }

    #endregion
}
