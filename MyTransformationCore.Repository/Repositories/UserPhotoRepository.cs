using MyTransformationCore.Domain.Models;
using MyTransformationCore.Domain.Configs;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public class UserPhotoRepository(IMongoClient mongoClient) : IUserPhotoRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<UserPhoto> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<UserPhoto>("user_photo");

    #endregion

    #region snippet_Methods

    public async Task<IEnumerable<UserPhoto>> GetAllAsync(FilterDefinition<UserPhoto> filter)
        => await _collection.Find(filter).ToListAsync();

    public async Task<UserPhoto> GetAsync(FilterDefinition<UserPhoto> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    #endregion
}
