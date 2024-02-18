using Microsoft.AspNetCore.Mvc;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Web.Controllers;

[Route(ApiConfig.BasePath + "/v1/users/progress")]
[ApiController]
public class UserProgressController(IUserProgressRepository userProgressRepository, IUserProgressManager userProgressManager) : ControllerBase
{
    #region snippet_Properties

    private readonly IUserProgressRepository _userProgressRepository = userProgressRepository;

    private readonly IUserProgressManager _userProgressManager = userProgressManager;

    #endregion

    #region snipper_Methods

    [HttpGet("me")]
    public async Task<IActionResult> GetAllMeAsync([FromHeader(Name = "user-id")] string userId)
        => Ok(await _userProgressRepository.GetAllAsync(Builders<UserProgress>.Filter.Eq(up => up.UserId, userId)));

    [HttpGet("me/{id}")]
    public async Task<IActionResult> GetMeAsync([FromHeader(Name = "user-id")] string userId, [FromRoute] string id)
    {
        var userIdMatch = Builders<UserProgress>.Filter.Eq(up => up.UserId, userId);
        var idMatch = Builders<UserProgress>.Filter.Eq(up => up.Id, id);
        UserProgress userProgress = await _userProgressRepository.GetAsync(Builders<UserProgress>.Filter.And(userIdMatch, idMatch));

        if (userProgress is null) return NotFound(new { Message = "User progress not found" });

        return Ok(userProgress);
    }

    [HttpPost("me")]
    public async Task<IActionResult> CreateMeAsync([FromHeader(Name = "user-id")] string userId, [FromBody] UserProgressCreation userProgressCreation)
    {
        var userProgress = new UserProgress
        {
            UserId = userId,
            ExerciseId = userProgressCreation.ExerciseId,
            Weight = userProgressCreation.Weight,
            MeasurementUnit = userProgressCreation.MeasurementUnit,
            Moment = userProgressCreation.Moment,
            Reps = userProgressCreation.Reps,
            Comment = userProgressCreation.Comment
        };
        await _userProgressManager.CreateAsync(userProgress);

        return Created(ApiConfig.BasePath + "/v1/users/progress/" + userProgress.Id, userProgress);
    }

    [HttpPatch("me/{id}")]
    public async Task<IActionResult> UpdateMeAsync([FromHeader(Name = "user-id")] string userId, [FromRoute] string id, [FromBody] UserProgressUpdate userProgressUpdate)
    {
        var userIdMatch = Builders<UserProgress>.Filter.Eq(up => up.UserId, userId);
        var idMatch = Builders<UserProgress>.Filter.Eq(up => up.Id, id);
        UserProgress userProgress = await _userProgressRepository.GetAsync(Builders<UserProgress>.Filter.And(userIdMatch, idMatch));

        if (userProgress is null) return NotFound(new { Message = "User progress not found" });

        userProgress.ExerciseId ??= userProgressUpdate.ExerciseId;
        userProgress.Weight ??= userProgressUpdate.Weight;
        userProgress.Moment ??= userProgressUpdate.Moment;
        userProgress.MeasurementUnit ??= userProgressUpdate.MeasurementUnit;
        userProgress.Reps ??= userProgressUpdate.Reps;
        userProgress.Comment ??= userProgressUpdate.Comment;

        await _userProgressManager.UpdateAsync(Builders<UserProgress>.Filter.And(userIdMatch, idMatch), userProgress);

        return Ok(userProgress);
    }

    [HttpDelete("me/{id}")]
    public async Task<IActionResult> DeleteMeAsync([FromHeader(Name = "user-id")] string userId, [FromRoute] string id)
    {
        var userIdMatch = Builders<UserProgress>.Filter.Eq(up => up.UserId, userId);
        var idMatch = Builders<UserProgress>.Filter.Eq(up => up.Id, id);
        UserProgress userProgress = await _userProgressRepository.GetAsync(Builders<UserProgress>.Filter.And(userIdMatch, idMatch));

        if (userProgress is null) return NotFound(new { Message = "User progress not found" });

        await _userProgressManager.DeleteAsync(Builders<UserProgress>.Filter.And(userIdMatch, idMatch));

        return NoContent();
    }

    #endregion
}
