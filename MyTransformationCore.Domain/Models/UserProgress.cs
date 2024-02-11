using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class UserProgress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty(nameof(Id))]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("user_id")]
    [JsonProperty(nameof(UserId))]
    public string UserId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("exercise_id")]
    [JsonProperty(nameof(ExerciseId))]
    public string ExerciseId { get; set; }

    [BsonElement("weight")]
    [JsonProperty(nameof(Weight))]
    public int? Weight { get; set; }

    [BsonElement("measurement_unit")]
    [JsonProperty(nameof(MeasurementUnit))]
    public string MeasurementUnit { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("moment")]
    [JsonProperty(nameof(Moment))]
    public DateTime? Moment { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty(nameof(UpdatedAt))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class UserProgressCreation
{
    [Required]
    [JsonProperty(nameof(ExerciseId))]
    public string ExerciseId { get; set; }

    [Required]
    [JsonProperty(nameof(Weight))]
    public int Weight { get; set; }

    [Required]
    [JsonProperty(nameof(MeasurementUnit))]
    public string MeasurementUnit { get; set; }

    [Required]
    [JsonProperty(nameof(Moment))]
    public DateTime Moment { get; set; }
}

public class UserProgressUpdate
{
    [JsonProperty(nameof(ExerciseId))]
    public string ExerciseId { get; set; }

    [JsonProperty(nameof(Weight))]
    public int? Weight { get; set; }

    [JsonProperty(nameof(MeasurementUnit))]
    public string MeasurementUnit { get; set; }

    [JsonProperty(nameof(Moment))]
    public DateTime? Moment { get; set; }
}
