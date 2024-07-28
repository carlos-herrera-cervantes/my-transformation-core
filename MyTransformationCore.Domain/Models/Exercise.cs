using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using MyTransformationCore.Domain.Configs;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class Exercise
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty(nameof(Id))]
    public string Id { get; set; }

    [BsonElement("name")]
    [JsonProperty(nameof(Name))]
    public string Name { get; set; }

    [BsonElement("image")]
    [JsonProperty(nameof(Image))]
    public string Image { get; set; }

    [BsonElement("muscle_groups")]
    [JsonProperty(nameof(MuscleGroups))]
    public string MuscleGroups { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty(nameof(UpdatedAt))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Try to set the full image path to the S3 bucket path.
    /// If the image path is a fallback image, it will be set to the default host.
    /// </summary>
    public void TrySetFullImagePath()
    {
        bool fallbackImage = this.Image.StartsWith("/images");
        this.Image = fallbackImage ?
            $"{ApiConfig.DefaultHost}{this.Image}" :
            $"{S3Config.DefaultEndpoint}/{S3Config.DefaultBucket}/{this.Image}";
    }
}

public class ExerciseCreation
{
    [Required]
    public string Name { get; set; }

    public IFormFile Image { get; set; }

    [Required]
    public string MuscleGroups { get; set; }
}

public class ExerciseUpdate
{
    public string Name { get; set; }

    public IFormFile Image { get; set; }

    public string MuscleGroups { get; set; }
}
