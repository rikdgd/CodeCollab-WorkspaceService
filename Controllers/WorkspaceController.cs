using System.Text.Json;
using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;
using CodeCollab___WorkspaceService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CodeCollab___WorkspaceService.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkspaceController : ControllerBase
{
    private readonly Messenger _messenger;

    public WorkspaceController(Messenger messenger)
    {
        _messenger = messenger;
    }
    
    
    [HttpGet("GetWorkspace", Name = "GetWorkspace")]
    public IActionResult GetWorkspace(string workspaceId)
    {
        WorkspaceService service = new WorkspaceService();
        WorkspaceModel? workspace = service.GetWorkspaceById(workspaceId);
        
        if (workspace == null) return BadRequest("Could not find workspace with the given id.");
        return Ok(workspace);
    }
}
