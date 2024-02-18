namespace MyTransformationCore.Services.Aws;

public interface IS3Service
{
    Task<string> PutObjectAsync(string filename, string rootPath, Stream stream);

    Task DeleteObjectAsync(string filename);
}
