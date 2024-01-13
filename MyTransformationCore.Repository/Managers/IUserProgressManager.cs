using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public interface IUserProgressManager
{
    Task CreateAsync(UserProgress userProgress);

    Task UpdateAsync(FilterDefinition<UserProgress> filter, UserProgress userProgress);

    Task DeleteAsync(FilterDefinition<UserProgress> filter);
}
