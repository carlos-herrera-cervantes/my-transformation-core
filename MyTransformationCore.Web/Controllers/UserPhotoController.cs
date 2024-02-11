using Microsoft.AspNetCore.Mvc;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Web.Controllers;

[Route(ApiConfig.BasePath + "/v1/users/photos")]
[ApiController]
public class UserPhotoController(IUserPhotoRepository userRepository, IUserPhotoManager userManager) : ControllerBase
{
    #region snippet_Properties

    private readonly IUserPhotoRepository _userPhotoRepository = userRepository;

    private readonly IUserPhotoManager _userPhotoManager = userManager;

    #endregion

    #region snippet_Methods

    [HttpGet("me")]
    public async Task<IActionResult> GetMeAsync([FromHeader(Name = "user-id")] string userId)
    {
        IEnumerable<UserPhoto> photos = await _userPhotoRepository.GetAllAsync(Builders<UserPhoto>.Filter.Eq(up => up.UserId, userId));
        return Ok(photos);
    }

    [HttpPost("me")]
    public async Task<IActionResult> CreateMeAsync([FromHeader(Name = "user-id")] string userId, [FromForm] UserPhotoCreation userPhotoCreation)
    {
        UserPhoto photo = new()
        {
            UserId = userId,
            Path = userPhotoCreation.Image.FileName,
            Moment = userPhotoCreation.Moment
        };

        await _userPhotoManager.CreateAsync(photo);

        return Created(ApiConfig.BasePath + "/v1/users/photos/" + photo.Id, photo);
    }

    [HttpDelete("me/{id}")]
    public async Task<IActionResult> DeleteMeAsync([FromHeader(Name = "user-id")] string userId, [FromRoute] string id)
    {
        var firstCondition = Builders<UserPhoto>.Filter.Eq(up => up.UserId, userId);
        var secondCondition = Builders<UserPhoto>.Filter.Eq(up => up.Id, id);

        UserPhoto photo = await _userPhotoRepository.GetAsync(Builders<UserPhoto>.Filter.And(firstCondition, secondCondition));

        if (photo is null) return NotFound(new { Message = "Photo not found" });

        await _userPhotoManager.DeleteAsync(Builders<UserPhoto>.Filter.And(firstCondition, secondCondition));

        return NoContent();
    }

    #endregion
}
