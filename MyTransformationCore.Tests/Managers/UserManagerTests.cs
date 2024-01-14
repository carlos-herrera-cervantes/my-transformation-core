using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;
using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;

using MongoDB.Driver;

namespace MyTransformationCore.Tests.Managers;

[ExcludeFromCodeCoverage]
[Collection(nameof(UserManager))]
public class UserManagerTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient = new MongoClient(DatabaseConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should execute all the processes that make up the CRUD")]
    public async Task Crud()
    {
        var userManager = new UserManager(_mongoClient);
        var userRepository = new UserRepository(_mongoClient);

        await userManager.CreateAsync(new User
        {
            FirstName = "Test",
            LastName = "User",
            Birthdate = DateTime.UtcNow,
            ProfilePicture = "/profile.png"
        });

        User user = await userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.FirstName, "Test"));
        user.LastName = "User New";

        await userManager.UpdateAsync(Builders<User>.Filter.Eq(u => u.FirstName, "Test"), user);
        await userManager.DeleteAsync(Builders<User>.Filter.Eq(u => u.FirstName, "Test"));
    }

    #endregion
}
