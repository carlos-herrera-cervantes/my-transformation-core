namespace MyTransformationCore.Domain.Configs;

public static class S3Config
{
    public static readonly string DefaultBucket = Environment.GetEnvironmentVariable("DEFAULT_S3_BUCKET");

    public static readonly string DefaultEndpoint = Environment.GetEnvironmentVariable("DEFAULT_S3_ENDPOINT");

    public static readonly string AccessKey = Environment.GetEnvironmentVariable("DEFAULT_S3_ACCESS_KEY");

    public static readonly string SecretKey = Environment.GetEnvironmentVariable("DEFAULT_S3_SECRET_KEY");

    public static readonly string Region = Environment.GetEnvironmentVariable("DEFAULT_S3_REGION");
}
