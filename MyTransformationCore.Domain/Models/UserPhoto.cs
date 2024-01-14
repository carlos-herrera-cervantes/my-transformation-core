using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class UserPhoto
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

    [BsonElement("path")]
    [JsonProperty("path")]
    public string Path { get; set; }

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
