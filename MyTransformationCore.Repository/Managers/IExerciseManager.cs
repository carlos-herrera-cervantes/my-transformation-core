using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Managers;

public interface IExerciseManager
{
    Task CreateAsync(Exercise exercise);

    Task UpdateAsync(FilterDefinition<Exercise> filter, Exercise exercise);

    Task DeleteAsync(FilterDefinition<Exercise> filter);
}
