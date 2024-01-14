using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Managers;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserManager))]
public class ExerciseManagerTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should execute all the processes that make up the CRUD")]
    public async Task Crud()
    {
        var exerciseManager = new ExerciseManager(_mongoClient);
        var exerciseRepository = new ExerciseRepository(_mongoClient);

        await exerciseManager.CreateAsync(new Exercise
        {
            Name = "Bench Press",
            Image = "/bench-press.png",
            MuscleGroups = "pecs, front deltoid"
        });

        Exercise exercise = await exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "Bench Press"));
        exercise.Name = "New Bench Press";

        await exerciseManager.UpdateAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "Bench Press"), exercise);
        await exerciseManager.DeleteAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "New Bench Press"));
    }

    #endregion
}
