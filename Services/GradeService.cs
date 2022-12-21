using Microsoft.Extensions.Options;
using Grupp4.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Grupp4.Services;

public class GradeService
{
    private readonly IMongoCollection<GradeModel> _gradesCollection;

    public GradeService(IOptions<GradeDbSettings> gradeDbSettings)
    {
        MongoClient client = new MongoClient(gradeDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(gradeDbSettings.Value.DatabaseName);
        _gradesCollection = database.GetCollection<GradeModel>(gradeDbSettings.Value.CollectionName);
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

    public async Task<GradeModel?> CreateAsync(GradeModel grade)
    {
        await _gradesCollection.InsertOneAsync(grade);
        return grade;
    }

    public async Task<UpdateResult> AddScoreAsync(string id, ScoreModel score)
    {
        FilterDefinition<GradeModel> filter = Builders<GradeModel>.Filter.Eq("Id", id);
        UpdateDefinition<GradeModel> update = Builders<GradeModel>.Update.AddToSet<ScoreModel>("Scores", score);
        return await _gradesCollection.UpdateOneAsync(filter, update);
    }

    public async Task<DeleteResult> DeleteAsync(string id)
    {
        FilterDefinition<GradeModel> filter = Builders<GradeModel>.Filter.Eq("Id", id);
        return await _gradesCollection.DeleteOneAsync(filter);
    }
}
