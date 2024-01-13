using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public interface IUserProgressRepository
{
    Task<IEnumerable<UserProgress>> GetAllAsync(FilterDefinition<UserProgress> filter);

    Task<UserProgress> GetAsync(FilterDefinition<UserProgress> filter);
}
