using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class UserProgress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("user_id")]
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("exercise_id")]
    [JsonProperty("exercise_id")]
    public string ExerciseId { get; set; }

    [BsonElement("weight_in_kilos")]
    [JsonProperty("weight_in_kilos")]
    public int WeightInKilos { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("moment")]
    [JsonProperty("moment")]
    public DateTime Moment { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
