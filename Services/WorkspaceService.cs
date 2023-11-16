using CodeCollab___WorkspaceService.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace CodeCollab___WorkspaceService.Services;

public class WorkspaceService
{
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
    

    public string? GetWorkspaceById(int id)
    {
        connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        
        try
        {
            var collection = this.mongoClient.GetDatabase("CodeCollab").GetCollection<BsonDocument>("Workspaces");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var document = collection.Find(filter).First();

            return document.AsString;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public void CreateWorkspace(WorkspaceModel workspaceModel)
    {
        
    }
}