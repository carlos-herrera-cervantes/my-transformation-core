using MyTransformationCore.Domain.Models;
using MyTransformationCore.Domain.Configs;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public class UserManager(IMongoClient mongoClient) : IUserManager
{
    #region snippet_Properties

    private readonly IMongoCollection<User> _collection =
        mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<User>("users");

    #endregion

    #region snippet_Methods

    public async Task CreateAsync(User user) => await _collection.InsertOneAsync(user);

    public async Task UpdateAsync(FilterDefinition<User> filter, User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteAsync(FilterDefinition<User> filter) => await _collection.DeleteOneAsync(filter);

    #endregion
}
