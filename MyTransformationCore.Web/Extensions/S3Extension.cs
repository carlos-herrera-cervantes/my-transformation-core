using MyTransformationCore.Domain.Configs;

using Amazon.Runtime;
using Amazon.S3;

namespace MyTransformationCore.Web.Extensions;

public static class S3Extension
{
    public static IServiceCollection AddS3Client(this IServiceCollection services)
    {
        var credentials = new BasicAWSCredentials(S3Config.AccessKey, S3Config.SecretKey);
        var config = new AmazonS3Config
        {
            ServiceURL = S3Config.DefaultEndpoint,
            UseHttp = true,
            ForcePathStyle = true,
            AuthenticationRegion = S3Config.Region
        };
        var s3Client = new AmazonS3Client(credentials, config);

        services.AddSingleton<IAmazonS3>(_ => s3Client);

        return services;
    }
}
