using MyTransformationCore.Domain.Models;
using MyTransformationCore.Domain.Configs;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public class ExerciseRepository(IMongoClient mongoClient) : IExerciseRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<Exercise> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<Exercise>("exercises");

    #endregion

    #region snippet_Methods

    public async Task<IEnumerable<Exercise>> GetAllAsync(FilterDefinition<Exercise> filter)
        => await _collection.Find(filter).ToListAsync();

    public async Task<Exercise> GetAsync(FilterDefinition<Exercise> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    public async Task<long> CountAsync(FilterDefinition<Exercise> filter)
        => await _collection.CountDocumentsAsync(filter);

    #endregion
}
