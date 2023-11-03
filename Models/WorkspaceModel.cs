namespace CodeCollab___WorkspaceService.Models;

public class WorkspaceModel
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public long OwnerId { get; private set; }

    public WorkspaceModel(int id, string name, int ownerId)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }
}