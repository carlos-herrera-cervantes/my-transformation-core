using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class Exercise
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [BsonElement("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [BsonElement("image")]
    [JsonProperty("image")]
    public string Image { get; set; }

    [BsonElement("muscle_groups")]
    [JsonProperty("muscle_groups")]
    public string MuscleGroups { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
