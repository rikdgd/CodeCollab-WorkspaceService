using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using MongoDB.Bson;

namespace WorkspaceTests.Mock;

public class WorkspaceServiceMock : IWorkspaceService
{
    public List<BsonDocument> workspaces { get; set; } = new();
    

    public WorkspaceModel? GetWorkspaceById(string id)
    {
        return new WorkspaceModel(id, "test-workspace", "22");
    }

    public void CreateWorkspace(WorkspaceModel workspaceModel)
    {
        try
        {
            var bsonModel = workspaceModel.ToBsonDocument();
            var newWorkspaces = this.workspaces.Append(bsonModel);
            this.workspaces = new List<BsonDocument>(newWorkspaces);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}