using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public interface IUserRepository
{
    Task<long> CountAsync(FilterDefinition<User> filter);

    Task<User> GetAsync(FilterDefinition<User> filter);

    Task<IEnumerable<User>> GetAllAsync(FilterDefinition<User> filter);
}
