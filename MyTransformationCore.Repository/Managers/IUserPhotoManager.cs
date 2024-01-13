using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public interface IUserPhotoManager
{
    Task CreateAsync(UserPhoto userPhoto);

    Task DeleteAsync(FilterDefinition<UserPhoto> filter);
}
