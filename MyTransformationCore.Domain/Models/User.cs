using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [BsonElement("first_name")]
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [BsonElement("last_name")]
    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("birthdate")]
    [JsonProperty("birthdate")]
    public DateTime Birthdate { get; set; }

    [BsonElement("profile_picture")]
    [JsonProperty("profile_picture")]
    public string ProfilePicture { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
