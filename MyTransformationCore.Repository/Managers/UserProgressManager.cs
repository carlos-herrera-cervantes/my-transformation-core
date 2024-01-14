using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public class UserProgressManager(IMongoClient mongoClient)
{
    #region snippet_Properties

    private readonly IMongoCollection<UserProgress> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<UserProgress>("user_progress");

    #endregion

    #region snippet_Methods

    public async Task CreateAsync(UserProgress userProgress) => await _collection.InsertOneAsync(userProgress);

    public async Task UpdateAsync(FilterDefinition<UserProgress> filter, UserProgress userProgress)
    {
        userProgress.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(filter, userProgress);
    }

    public async Task DeleteAsync(FilterDefinition<UserProgress> filter)
        => await _collection.DeleteOneAsync(filter);

    #endregion
}
