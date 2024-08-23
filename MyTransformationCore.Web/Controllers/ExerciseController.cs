using Microsoft.AspNetCore.Mvc;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Services.Aws;

using MongoDB.Driver;

namespace MyTransformationCore.Web.Controllers;

[Route(ApiConfig.BasePath + "/v1/exercise")]
[ApiController]
public class ExerciseController(IExerciseRepository exerciseRepository, IExerciseManager exerciseManager, IS3Service s3Service, ILogger<ExerciseController> logger) : ControllerBase
{
    #region snippet_Properties

    private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

    private readonly IExerciseManager _exerciseManager = exerciseManager;

    private readonly IS3Service _s3Service = s3Service;

    private readonly ILogger<ExerciseController> _logger = logger;

    #endregion

    #region snippet_Methods

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<Exercise> documents = await _exerciseRepository.GetAllAsync(Builders<Exercise>.Filter.Empty);
        var exercises = documents.Select(d =>
        {
            d.TrySetFullImagePath();
            return d;
        });

        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));
        
        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        exercise.TrySetFullImagePath();

        return Ok(exercise);
    }

    [HttpPost]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> CreateAsync([FromForm] ExerciseCreation exerciseCreation)
    {
        string imagePath = $"{ApiConfig.DefaultHost}{AssetsConfig.FallbackExeriseImage}";

        if (exerciseCreation.Image is not null)
        {
            var imageStream = exerciseCreation.Image.OpenReadStream();
            imagePath = await _s3Service.PutObjectAsync(filename: exerciseCreation.Image.FileName, rootPath: "exercises", stream: imageStream);
        }

        var exercise = new Exercise
        {
            Name = exerciseCreation.Name.Trim(),
            Image = exerciseCreation.Image is not null ? $"exercises/{exerciseCreation.Image.FileName}" : AssetsConfig.FallbackExeriseImage,
            MuscleGroups = exerciseCreation.MuscleGroups.Trim()
        };

        IActionResult actionResult = await ActionResultOnErrorAsync(_exerciseManager.CreateAsync, exercise);

        if (actionResult is not null) return actionResult;

        exercise.Image = imagePath;

        return Created(ApiConfig.BasePath + $"/v1/exercise/{exercise.Id}", exercise);
    }

    [HttpPut("{id}")]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromForm] ExerciseUpdate exerciseUpdate)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        if (exerciseUpdate.Image is not null && !exercise.FallbackImage())
        {
            var imageStream = exerciseUpdate.Image.OpenReadStream();
            await _s3Service.PutObjectAsync(filename: exerciseUpdate.Image.FileName, rootPath: "exercises", stream: imageStream);
            exercise.Image = $"exercises/{exerciseUpdate.Image.FileName}";
        }

        exercise.Name = exerciseUpdate.Name ?? exercise.Name;
        exercise.MuscleGroups = exerciseUpdate.MuscleGroups ?? exercise.MuscleGroups;

        await _exerciseManager.UpdateAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id), exercise);

        exercise.TrySetFullImagePath();

        return Ok(exercise);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        if (exercise is null) return NotFound(new { Message = "Exercise not found" });

        if (!exercise.FallbackImage())
        {
            await _s3Service.DeleteObjectAsync(filename: exercise.Image);
        }

        await _exerciseManager.DeleteAsync(Builders<Exercise>.Filter.Eq(e => e.Id, id));

        return NoContent();
    }

    #endregion

    #region snippet_PrivateMethods

    /// <summary>
    /// This method is used to handle exceptions in the controller.
    /// </summary>
    /// <param name="fn">The function that will be executed</param>
    /// <param name="exercise">Exercise object</param>
    /// <returns>Http response</returns>
    private async Task<IActionResult> ActionResultOnErrorAsync(Func<Exercise, Task> fn, Exercise exercise)
    {
        try
        {
            await fn(exercise);
        }
        catch (MongoWriteException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(ExerciseController) + nameof(fn) + ex);
            return StatusCode(500, new { Message = "Internal server error" });
        }

        return null;
    }

    #endregion
}
