using MyTransformationCore.Domain.Models;
using MyTransformationCore.Domain.Configs;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public class UserRepository(IMongoClient mongoClient) : IUserRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<User> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<User>("users");

    #endregion

    #region snippet_Methods

    public async Task<long> CountAsync(FilterDefinition<User> filter)
        => await _collection.CountDocumentsAsync(filter);

    public async Task<User> GetAsync(FilterDefinition<User> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetAllAsync(FilterDefinition<User> filter)
        => await _collection.Find(filter).ToListAsync();

    #endregion
}
