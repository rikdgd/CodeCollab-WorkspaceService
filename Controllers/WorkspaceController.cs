using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCollab___WorkspaceService.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkspaceController : ControllerBase
{
    [HttpGet(Name = "GetWorkspace")]
    public IActionResult GetWorkspace(string workspaceId)
    {
        WorkspaceService service = new WorkspaceService();
        WorkspaceModel? workspace = service.GetWorkspaceById(workspaceId);
        
        if (workspace == null) return BadRequest("Could not find workspace with the given id.");
        return Ok(workspace);
    }

    [HttpPost(Name = "CreateWorkspace")]
    public IActionResult CreateWorkspace([FromBody] WorkspaceModel workspace)
    {
        return Ok("Workspace created successfully");
    }
}