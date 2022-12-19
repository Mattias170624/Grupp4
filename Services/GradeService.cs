using Microsoft.Extensions.Options;
using Grupp4.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Grupp4.Services;

public class GradeService
{
    private readonly IMongoCollection<GradeModel> _gradesCollection;

    public GradeService(IOptions<MongoDbOptions> mongoDbOptions)
    {
        MongoClient client = new MongoClient(mongoDbOptions.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbOptions.Value.DatabaseName);
        _gradesCollection = database.GetCollection<GradeModel>(mongoDbOptions.Value.CollectionName);
    }

    public async Task<List<GradeModel>> GetAsync()
    {
        return await _gradesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<GradeModel?> GetById(string id)
    {
        FilterDefinition<GradeModel> filter = Builders<GradeModel>.Filter.Eq("Id", id);
        return await _gradesCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(GradeModel grade)
    {
        await _gradesCollection.InsertOneAsync(grade);
        return;
    }

    public async Task AddScoreAsync(string id, ScoreModel score)
    {
        FilterDefinition<GradeModel> filter = Builders<GradeModel>.Filter.Eq("Id", id);
        UpdateDefinition<GradeModel> update = Builders<GradeModel>.Update.AddToSet<ScoreModel>("Scores", score);
        await _gradesCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<GradeModel> filter = Builders<GradeModel>.Filter.Eq("Id", id);
        await _gradesCollection.DeleteOneAsync(filter);
        return;
    }
}
