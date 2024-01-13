using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetAllAsync(FilterDefinition<Exercise> filter);

    Task<Exercise> GetAsync(FilterDefinition<Exercise> filter);

    Task<long> CountAsync(FilterDefinition<Exercise> filter);
}
