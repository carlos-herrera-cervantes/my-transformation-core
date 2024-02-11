using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
}

public class ExerciseCreation
{
    [Required]
    public string Name { get; set; }

    [Required]
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
