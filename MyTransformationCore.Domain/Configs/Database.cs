namespace MyTransformationCore.Domain.Configs;

public static class DatabaseConfig
{
    public static readonly string DefaultDb = Environment.GetEnvironmentVariable("DEFAULT_DB");

    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
}
