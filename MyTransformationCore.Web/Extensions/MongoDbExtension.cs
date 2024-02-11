using MongoDB.Driver;
using MyTransformationCore.Domain.Configs;

namespace MyTransformationCore.Web.Extensions;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDbClient(this IServiceCollection services)
    {
        var mongoClient = new MongoClient(DatabaseConfig.ConnectionString);
        services.AddSingleton<IMongoClient>(_ => mongoClient);
        return services;
    }
}
