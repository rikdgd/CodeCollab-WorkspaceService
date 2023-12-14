using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;


namespace CodeCollab___WorkspaceService.Services;

public class WorkspaceService : IWorkspaceService
{
    private string databaseName = "CodeCollab-testing";
    private string collectionName = "Workspaces";
    
    private MongoClient mongoClient;

    private string? connectionString;

    public WorkspaceService()
    {
        connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        if (connectionString != null)
        {
            mongoClient = new MongoClient(connectionString);
        }
        else
        {
            Environment.Exit(0);
        }
    }
    

    public WorkspaceModel? GetWorkspaceById(string id)
    {
        try
        {
            var collection = this.mongoClient.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = collection.Find(filter).First();

            WorkspaceModel? workspaceModel = BsonSerializer.Deserialize<WorkspaceModel>(document);
            return workspaceModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }


    public async Task<List<WorkspaceModel>> GetAllWorkspacesByUserId(string id)
    {
        try
        {
            var collection = this.mongoClient.GetDatabase(databaseName).GetCollection<WorkspaceModel>(collectionName);
            var filter = Builders<WorkspaceModel>.Filter.Eq<string>(r => r.OwnerId, id);
            return await collection.Find(filter).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<WorkspaceModel>();
    }
    

    public void CreateWorkspace(WorkspaceModel workspaceModel)
    {
        var collection = mongoClient.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);
        BsonDocument workspaceData = workspaceModel.ToBsonDocument();
        
        collection.InsertOne(workspaceData);
    }
}