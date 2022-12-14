using Grupp4.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Grupp4.Services;

public class PlanetDBService {
    
    private readonly IMongoCollection<Planets> _playlistCollection;

    public PlanetDBService(IOptions<PlanetDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Planets>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Planets planets) {
        await _playlistCollection.InsertOneAsync(planets);
        return;
    }

    public async Task<List<Planets>> GetAsync() {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToPlaylistAsync(string id, string movieId) {
        FilterDefinition<Planets> filter = Builders<Planets>.Filter.Eq("Id", id);
        UpdateDefinition<Planets> update = Builders<Planets>.Update.AddToSet<string>("movieId", movieId);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Planets> filter = Builders<Planets>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
        return;
    }
}
