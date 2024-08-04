using MyTransformationCore.Domain.Configs;
using MyTransformationCore.Domain.Models;

using MongoDB.Driver;

namespace MyTransformationCore.Repository.Repositories;

public class UserProgressRepository(IMongoClient mongoClient) : IUserProgressRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<UserProgress> _collection
        = mongoClient.GetDatabase(DatabaseConfig.DefaultDb).GetCollection<UserProgress>("user_progress");

    #endregion

    #region snippet_Methods

    public async Task<IEnumerable<UserProgress>> GetAllAsync(FilterDefinition<UserProgress> filter, Pageable pageable)
    {
        var from = Builders<UserProgress>.Filter.Gte(up => up.Moment, pageable.From);
        var to = Builders<UserProgress>.Filter.Lte(up => up.Moment, pageable.To);
        
        if (pageable.NoExercise)
        {
            return await _collection
                .Find(Builders<UserProgress>.Filter.And(filter, from, to))
                .SortByDescending(up => up.Moment)
                .Skip(pageable.Page * pageable.PageSize)
                .Limit(pageable.PageSize)
                .ToListAsync();
        }

        var query = _collection
            .Aggregate()
            .SortByDescending(up => up.Moment)
            .Match(Builders<UserProgress>.Filter.And(filter, from, to))
            .Skip(pageable.Page * pageable.PageSize)
            .Limit(pageable.PageSize)
            .Lookup("exercises", "exercise_id", "_id", "Exercise");

        return await query.As<UserProgress>().ToListAsync();
    }

    public async Task<UserProgress> GetAsync(FilterDefinition<UserProgress> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    #endregion
}
