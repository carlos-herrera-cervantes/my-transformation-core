using Microsoft.AspNetCore.Http;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MyTransformationCore.Domain.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    [JsonProperty(nameof(Id))]
    public string Id { get; set; }

    [BsonElement("first_name")]
    [JsonProperty(nameof(FirstName))]
    public string FirstName { get; set; }

    [BsonElement("last_name")]
    [JsonProperty(nameof(LastName))]
    public string LastName { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("birthdate")]
    [JsonProperty(nameof(Birthdate))]
    public DateTime? Birthdate { get; set; }

    [BsonElement("profile_picture")]
    [JsonProperty(nameof(ProfilePicture))]
    public string ProfilePicture { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("created_at")]
    [JsonProperty(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("updated_at")]
    [JsonProperty(nameof(UpdatedAt))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class UserUpdate
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? Birthdate { get; set; }

    public IFormFile ProfilePicture { get; set; }
}
