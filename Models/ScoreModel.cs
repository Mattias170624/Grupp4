using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Grupp4.Models;

public class ScoreModel
{

    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [BsonElement("score")]
    [JsonPropertyName("score")]
    public double Score { get; set; }
}