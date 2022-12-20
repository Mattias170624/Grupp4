using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Grupp4.Models;

public class Playlist
{
    /// <summary>each id is unique</summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>each user has a username</summary>
    [BsonElement("username")]
    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

    /// <summary>each item is a movie id</summary>
    [BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string> Items { get; set; } = null!;
}