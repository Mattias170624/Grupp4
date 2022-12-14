using Grupp4.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Grupp4.Services;

public class PlanetDBService {
    
    private readonly IMongoCollection<Planets> _planetsCollection;

    public PlanetDBService(IOptions<PlanetDBSettings> planetDBSettings) {
        MongoClient client = new MongoClient(planetDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(planetDBSettings.Value.DatabaseName);
        _planetsCollection = database.GetCollection<Planets>(planetDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Planets planets) {
        await _planetsCollection.InsertOneAsync(planets);
        return;
    }

    public async Task<List<Planets>> GetAsync() {
        return await _planetsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToPlanetsAsync(string id, string movieId) {
        FilterDefinition<Planets> filter = Builders<Planets>.Filter.Eq("Id", id);
        UpdateDefinition<Planets> update = Builders<Planets>.Update.AddToSet<string>("movieId", movieId);
        await _planetsCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Planets> filter = Builders<Planets>.Filter.Eq("Id", id);
        await _planetsCollection.DeleteOneAsync(filter);
        return;
    }
}
