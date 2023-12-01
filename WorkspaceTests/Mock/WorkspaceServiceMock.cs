using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using MongoDB.Bson;

namespace WorkspaceTests.Mock;

public class WorkspaceServiceMock : IWorkspaceService
{
    public BsonDocument[]? workspaces { get; set; } = null;
    
    
    public WorkspaceModel? GetWorkspaceById(string id)
    {
        return new WorkspaceModel(id, "test-workspace", 22);
    }

    public void CreateWorkspace(WorkspaceModel workspaceModel)
    {
        try
        {
            this.workspaces.Append(workspaceModel.ToBsonDocument());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}