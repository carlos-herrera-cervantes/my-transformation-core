namespace MyTransformationCore.Domain.Configs;

public static class ApiConfig
{
    public const string BasePath = "/api/my-transformation-core";

    public static readonly string DefaultHost = Environment.GetEnvironmentVariable("HOST_APPLICATION");

    public static readonly string Timezone = Environment.GetEnvironmentVariable("TIMEZONE");
}

public static class AssetsConfig
{
    public const string FallbackExeriseImage = "/images/no-image-min.jpg";
}
