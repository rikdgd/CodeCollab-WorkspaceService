namespace CodeCollab___WorkspaceService.Models;

public class WorkspaceModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long OwnerId { get; set; }

    
    public WorkspaceModel() 
    {
        
    }

    public WorkspaceModel(int id, string name, int ownerId)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }
}