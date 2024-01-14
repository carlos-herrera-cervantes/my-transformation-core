using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Collection(nameof(ExerciseRepository))]
public class ExerciseRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return an empty list")]
    public async Task GetAllAsyncShouldReturnEmptyList()
    {
        var exerciseRepository = new ExerciseRepository(_mongoClient);
        IEnumerable<Exercise> exercises = await exerciseRepository.GetAllAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "bad"));
        Assert.Empty(exercises);
    }

    [Fact(DisplayName = "Should return null")]
    public async Task GetAsyncShouldReturnNull()
    {
        var exerciseRepository = new ExerciseRepository(_mongoClient);
        Exercise exercise = await exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "bad"));
        Assert.Null(exercise);
    }

    [Fact(DisplayName = "Should return 0")]
    public async Task CountAsyncShouldReturnZero()
    {
        var exerciseRepository = new ExerciseRepository(_mongoClient);
        long totalDocs = await exerciseRepository.CountAsync(Builders<Exercise>.Filter.Eq(e => e.Name, "bad"));
        Assert.True(totalDocs == 0);
    }

    #endregion
}
