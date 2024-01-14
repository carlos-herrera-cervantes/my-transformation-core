using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserProgressRepository))]
public class UserProgressRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return an empty list")]
    public async Task GetAllAsyncShouldReturnEmptyList()
    {
        var userProgressRepository = new UserProgressRepository(_mongoClient);
        IEnumerable<UserProgress> progress = await userProgressRepository.GetAllAsync(Builders<UserProgress>.Filter.Eq(up => up.ExerciseId, "65a2e22af773a5ce93afbd59"));
        Assert.Empty(progress);
    }

    #endregion
}
