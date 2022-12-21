using Grupp4.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Grupp4.Services;

public class MoviesDBService {

    private readonly IMongoCollection<Movies> _movieCollection; 

    public MoviesDBService(IOptions<MoviesDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _movieCollection = database.GetCollection<Movies>(mongoDBSettings.Value.CollectionName); 
    }

    public async Task CreateAsync(Movies movies) {
        await _movieCollection.InsertOneAsync(movies);
        return;
    }

    public async Task<List<Movies>> GetAsync() {
        return await _movieCollection.Find(new BsonDocument()).ToListAsync();
    }

     public async Task<Movies> GetAsyncId(string id)
    {
        FilterDefinition<Movies> filter = Builders<Movies>.Filter.Eq("Id", id);
        return await _movieCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddToMoviesAsync(string id, string movieId) {
        FilterDefinition<Movies> filter = Builders<Movies>.Filter.Eq("Id", id);
        UpdateDefinition<Movies> update = Builders<Movies>.Update.AddToSet<string>("movieId", movieId);
        await _movieCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Movies> filter = Builders<Movies>.Filter.Eq("Id", id);
        await _movieCollection.DeleteOneAsync(filter);
        return;
    }
}