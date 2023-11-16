using System.Text.Json;
using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCollab___WorkspaceService.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkspaceController : ControllerBase
{
    [HttpGet(Name = "GetWorkspace")]
    public IActionResult GetWorkspace()
    {
        // return Ok(new WorkspaceModel(1, "test-workspace", 23));
        // WorkspaceModel model = new WorkspaceModel(33, "test123", 47);
        // return Ok(JsonSerializer.Serialize(model)); // {"Id":33,"Name":"test123","OwnerId":47}


        WorkspaceService service = new WorkspaceService();
        var entry = service.GetWorkspaceById(92);
        return Ok(entry);
    }

    [HttpPost(Name = "CreateWorkspace")]
    public IActionResult CreateWorkspace([FromBody] WorkspaceModel workspace)
    {
        return Ok("Workspace created successfully");
    }
}