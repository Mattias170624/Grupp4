using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Grupp4.Models;

public class GradeModel
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; } = null;

    [BsonElement("student_id")]
    [JsonPropertyName("student_id")]
    public int StudentId { get; set; }

    [BsonElement("scores")]
    [JsonPropertyName("scores")]
    public List<ScoreModel> Scores { get; set; } = null!;

    [BsonElement("class_id")]
    [JsonPropertyName("class_id")]
    public int ClassId { get; set; }
}