using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public class ExerciseManager(IMongoClient mongoClient)
{
    #region snippet_Properties

    private readonly IMongoCollection<Exercise> _collection =
        mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<Exercise>("exercises");

    #endregion

    #region snippet_Methods

    public async Task CreateAsync(Exercise exercise) => await _collection.InsertOneAsync(exercise);

    public async Task UpdateAsync(FilterDefinition<Exercise> filter, Exercise exercise)
    {
        exercise.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(filter, exercise);
    }

    public async Task DeleteAsync(FilterDefinition<Exercise> filter) => await _collection.DeleteOneAsync(filter);

    #endregion
}
