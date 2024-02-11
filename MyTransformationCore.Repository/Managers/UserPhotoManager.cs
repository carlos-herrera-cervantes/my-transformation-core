using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public class UserPhotoManager(IMongoClient mongoClient) : IUserPhotoManager
{
    #region snippet_Properties

    private readonly IMongoCollection<UserPhoto> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<UserPhoto>("user_photo");

    #endregion

    #region snippet_Methods

    public async Task CreateAsync(UserPhoto userPhoto) => await _collection.InsertOneAsync(userPhoto);

    public async Task DeleteAsync(FilterDefinition<UserPhoto> filter) => await _collection.DeleteOneAsync(filter);

    #endregion
}
