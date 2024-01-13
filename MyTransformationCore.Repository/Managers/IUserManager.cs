using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public interface IUserManager
{
    Task CreateAsync(User user);

    Task UpdateAsync(FilterDefinition<User> filter, User user);

    Task DeleteAsync(FilterDefinition<User> filter);
}
