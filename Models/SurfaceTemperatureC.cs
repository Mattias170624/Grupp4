using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Grupp4.Models;

public class SurfaceTemperatureC {

    [BsonElement("min")]
    [JsonPropertyName("min")]
    public int Min { get; set; }

    [BsonElement("max")]
    [JsonPropertyName("max")]
    public int Max { get; set; }

    [BsonElement("mean")]
    [JsonPropertyName("mean")]
    public int Mean { get; set; }
}