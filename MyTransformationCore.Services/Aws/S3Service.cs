using MyTransformationCore.Domain.Configs;

using Amazon.S3;
using Amazon.S3.Model;

namespace MyTransformationCore.Services.Aws;

public class S3Service(IAmazonS3 s3Client) : IS3Service
{
    #region snippet_Properties

    private readonly IAmazonS3 _s3Client = s3Client;

    #endregion

    #region snippet_Methods

    public async Task<string> PutObjectAsync(string filename, string rootPath, Stream stream)
    {
        var request = new PutObjectRequest
        {
            InputStream = stream,
            BucketName = S3Config.DefaultBucket,
            Key = $"{rootPath}/{filename}"
        };

        await _s3Client.PutObjectAsync(request);

        return $"{S3Config.DefaultEndpoint}/{S3Config.DefaultBucket}/{rootPath}/{filename}";
    }

    public async Task DeleteObjectAsync(string filename)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = S3Config.DefaultBucket,
            Key = filename
        };

        await _s3Client.DeleteObjectAsync(request);
    }

    #endregion
}
