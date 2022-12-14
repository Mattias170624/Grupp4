using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Grupp4.Models;

public class Planets {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string name { get; set; } = null!;
    public int orderFromSun { get; set; }
    public bool hasRings { get; set; }
    public List<string> mainAtmoshpere { get; set; } = null!;

    [BsonElement("surfaceTemperatureC")]
    [JsonPropertyName("surfaceTemperatureC")]
    public List<string> surfaceTemperatureC { get; set; } = null!;
}