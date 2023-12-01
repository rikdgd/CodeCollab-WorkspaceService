using CodeCollab___WorkspaceService.Models;

namespace CodeCollab___WorkspaceService.Interfaces;

public interface IWorkspaceService
{
    public WorkspaceModel? GetWorkspaceById(string id);
    void CreateWorkspace(WorkspaceModel workspace);
}