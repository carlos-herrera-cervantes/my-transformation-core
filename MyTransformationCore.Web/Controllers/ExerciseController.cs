using Microsoft.AspNetCore.Mvc;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Web.Controllers;

[Route(ApiConfig.BasePath + "/v1/exercise")]
[ApiController]
public class ExerciseController(IExerciseRepository exerciseRepository, IExerciseManager exerciseManager) : ControllerBase
{
    #region snippet_Properties

    private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

    private readonly IExerciseManager _exerciseManager = exerciseManager;

    #endregion

    #region snippet_Methods

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _exerciseRepository.GetAllAsync(Builders<Exercise>.Filter.Empty));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));
        
        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        return Ok(exercise);
    }

    [HttpPost]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> CreateAsync([FromForm] ExerciseCreation exerciseCreation)
    {
        var exercise = new Exercise
        {
            Name = exerciseCreation.Name,
            Image = exerciseCreation.Image.FileName,
            MuscleGroups = exerciseCreation.MuscleGroups
        };

        await _exerciseManager.CreateAsync(exercise);

        return Created(ApiConfig.BasePath + $"/v1/exercise/{exercise.Id}", exercise);
    }

    [HttpPut("{id}")]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromForm] ExerciseUpdate exerciseUpdate)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        exercise.Image ??= exerciseUpdate.Image?.FileName;
        exercise.Name ??= exerciseUpdate.Name;
        exercise.MuscleGroups ??= exerciseUpdate.MuscleGroups;

        await _exerciseManager.UpdateAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id), exercise);

        return Ok(exercise);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        await _exerciseManager.DeleteAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        return NoContent();
    }

    #endregion
}
