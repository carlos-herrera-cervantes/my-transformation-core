using Microsoft.AspNetCore.Http;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class UserPhoto
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

    [BsonElement("path")]
    [JsonProperty(nameof(Path))]
    public string Path { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("moment")]
    [JsonProperty(nameof(Moment))]
    public DateTime Moment { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty(nameof(UpdatedAt))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class UserPhotoCreation
{
    public IFormFile Image { get; set; }

    public DateTime Moment { get; set; }
}
