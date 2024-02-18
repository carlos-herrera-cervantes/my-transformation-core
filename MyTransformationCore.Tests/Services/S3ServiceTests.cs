using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using Xunit;

using MyTransformationCore.Services.Aws;
using MyTransformationCore.Domain.Configs;

using Moq;
using Amazon.S3;
using Amazon.S3.Model;

namespace MyTransformationCore.Tests.Services;

[Collection("Services")]
[ExcludeFromCodeCoverage]
public class S3ServiceTests
{
    #region snippet_Properties

    private readonly Mock<IAmazonS3> _mockAmazonS3 = new();

    private readonly Mock<IFormFile> _mockFormFile = new();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = nameof(PutObjectAsyncShouldReturnString))]
    public async Task PutObjectAsyncShouldReturnString()
    {
        _mockAmazonS3
            .Setup(as3 => as3.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PutObjectResponse());
        var s3Service = new S3Service(_mockAmazonS3.Object);

        string imagePath = await s3Service.PutObjectAsync(
            filename: "exercise.jpg",
            rootPath: "exercises",
            stream: _mockFormFile.Object.OpenReadStream()
        );

        _mockAmazonS3.Verify(as3 => as3.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        var expectedImagePath = $"{S3Config.DefaultEndpoint}/{S3Config.DefaultBucket}/exercises/exercise.jpg";
        Assert.True(expectedImagePath == imagePath);
    }

    [Fact(DisplayName = nameof(DeleteObjectAsync))]
    public async Task DeleteObjectAsync()
    {
        _mockAmazonS3
            .Setup(as3 => as3.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteObjectResponse());
        var s3Service = new S3Service(_mockAmazonS3.Object);

        await s3Service.DeleteObjectAsync(filename: "exercise.jpg");

        _mockAmazonS3
            .Verify(as3 => as3.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}
