using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public class UserProgressRepository(IMongoClient mongoClient)
{
    #region snippet_Properties

    private readonly IMongoCollection<UserProgress> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<UserProgress>("user_progress");

    #endregion

    #region snippet_Methods

    public async Task<IEnumerable<UserProgress>> GetAllAsync(FilterDefinition<UserProgress> filter)
        => await _collection.Find(filter).ToListAsync();

    public async Task<UserProgress> GetAsync(FilterDefinition<UserProgress> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    #endregion
}
