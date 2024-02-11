using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Web.Controllers;

[Route(ApiConfig.BasePath + "/v1/users")]
[ApiController]
public class UserController(IUserRepository userRepository, IUserManager userManager) : ControllerBase
{
    #region snippet_Properties

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IUserManager _userManager = userManager;

    #endregion

    #region snippet_Methods

    [HttpGet("me")]
    public async Task<IActionResult> GetMeAsync([FromHeader(Name = "user-id")] string id)
    {
        User user = await _userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.Id, id));

        if (user is null) return NotFound(new { Message = "No profile exists" });

        return Ok(user);
    }

    [HttpPost]
    [ExcludeFromCodeCoverage]
    [Obsolete]
    public async Task<IActionResult> CreateAsync([FromBody] User user)
    {
        await _userManager.CreateAsync(user);
        return Created(ApiConfig.BasePath + $"/v1/users/{user.Id}", user);
    }

    [HttpPatch("me")]
    public async Task<IActionResult> UpdateMeAsync([FromHeader(Name = "user-id")] string id, [FromForm] UserUpdate userUpdate)
    {
        User user = await _userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.Id, id));

        if (user is null) return NotFound(new { Message = "No profile exists" });

        user.ProfilePicture ??= userUpdate.ProfilePicture?.FileName;
        user.FirstName ??= userUpdate.FirstName;
        user.LastName ??= userUpdate.LastName;
        user.Birthdate ??= userUpdate.Birthdate;

        await _userManager.UpdateAsync(Builders<User>.Filter.Eq(u => u.Id, id), user);

        return Ok(user);
    }

    #endregion
}
