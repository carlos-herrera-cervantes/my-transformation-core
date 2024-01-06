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

    [BsonElement("weight")]
    [JsonProperty("weight")]
    public int Weight { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("moment")]
    [JsonProperty("moment")]
    public DateTime Moment { get; set; }
}
