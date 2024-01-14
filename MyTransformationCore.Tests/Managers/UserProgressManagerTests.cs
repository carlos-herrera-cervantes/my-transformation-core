using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Managers;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserProgressManager))]
public class UserProgressManagerTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should execute all the processes that make up the CRUD")]
    public async Task Crud()
    {
        var userProgressManager = new UserProgressManager(_mongoClient);
        var userProgressRepository = new UserProgressRepository(_mongoClient);

        await userProgressManager.CreateAsync(new UserProgress
        {
            UserId = "65a2dc1cd99fab3b2e0646a7",
            ExerciseId = "65a2dc24b7a8fbf56f8fe4de",
            WeightInKilos = 10,
            Moment = DateTime.UtcNow
        });

        UserProgress userProgress = await userProgressRepository.GetAsync(Builders<UserProgress>.Filter.Eq(up => up.UserId, "65a2dc1cd99fab3b2e0646a7"));
        userProgress.WeightInKilos = 15;

        await userProgressManager.UpdateAsync(Builders<UserProgress>.Filter.Eq(up => up.UserId, "65a2dc1cd99fab3b2e0646a7"), userProgress);
        await userProgressManager.DeleteAsync(Builders<UserProgress>.Filter.Eq(up => up.UserId, "65a2dc1cd99fab3b2e0646a7"));
    }

    #endregion
}
