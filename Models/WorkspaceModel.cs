using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace CodeCollab___WorkspaceService.Models;

public class WorkspaceModel
{
    [JsonIgnore]
    public ObjectId? Id { get; set; }
    
    public string Name { get; set; }
    public int OwnerId { get; set; }
    
    public WorkspaceModel()
    {
        
    }

    public WorkspaceModel(string Id, string name, int ownerId)
    {
        this.Id = ObjectId.Parse(Id);
        this.Name = name;
        this.OwnerId = ownerId;
    }
}