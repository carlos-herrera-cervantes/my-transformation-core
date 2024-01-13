using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public interface IUserPhotoRepository
{
    Task<IEnumerable<UserPhoto>> GetAllAsync(FilterDefinition<UserPhoto> filter);

    Task<UserPhoto> GetAsync(FilterDefinition<UserPhoto> filter);
}
