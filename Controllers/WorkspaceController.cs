using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMessenger;

namespace CodeCollab___WorkspaceService.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkspaceController : ControllerBase
{
    private readonly Messenger _messenger;
    private readonly WorkspaceService _service = new();

    public WorkspaceController(Messenger messenger)
    {
        _messenger = messenger;
    }
    
    
    [HttpGet("GetWorkspace", Name = "GetWorkspace")]
    public IActionResult GetWorkspace(string workspaceId)
    {
        WorkspaceModel? workspace = _service.GetWorkspaceById(workspaceId);
        
        if (workspace == null) return BadRequest("Could not find workspace with the given id.");
        return Ok(workspace);
    }
    
    
    [HttpGet("GetWorkspaceByUserId", Name = "GetWorkspaceByUserId")]
    public async Task<IActionResult> GetWorkspaceByUserId(string userId)
    {
        List<WorkspaceModel>? workspaces = await _service.GetAllWorkspacesByUserId(userId);
        
        if (workspaces == null) return BadRequest("Could not find workspace with the given id.");
        return Ok(workspaces);
    }
}
