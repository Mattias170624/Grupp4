using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Grupp4.Models;

public class Planets {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [BsonElement("orderFromSun")]
    [JsonPropertyName("orderFromSun")]
    public int OrderFromSun { get; set; }

    [BsonElement("hasRings")]
    [JsonPropertyName("hasRings")]
    public bool HasRings { get; set; }

    public List<string> mainAtmoshpere { get; set; } = null!;

    [BsonElement("surfaceTemperatureC")]
    [JsonPropertyName("surfaceTemperatureC")]
    public List<string> SurfaceTemperatureC { get; set; } = null!;
}