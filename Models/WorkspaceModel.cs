using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeCollab___WorkspaceService.Models;

public class WorkspaceModel
{
    [JsonIgnore]
    [BsonId]
    [BsonIgnoreIfDefault]
    public ObjectId? Id { get; set; }
    
    public string Name { get; set; }
    public int OwnerId { get; set; }
    
    public WorkspaceModel()
    {
        
    }

    public WorkspaceModel(string id, string name, int ownerId)
    {
        this.Id = ObjectId.Parse(id);
        this.Name = name;
        this.OwnerId = ownerId;
    }
}