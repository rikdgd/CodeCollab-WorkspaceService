using CodeCollab___WorkspaceService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCollab___WorkspaceService.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkspaceController : ControllerBase
{
    [HttpGet(Name = "GetWorkspace")]
    public IActionResult GetWorkspace()
    {
        return Ok(new WorkspaceModel(1, "test-workspace", 23));
    }

    [HttpPost(Name = "CreateWorkspace")]
    public IActionResult CreateWorkspace([FromBody] WorkspaceModel workspace)
    {
        return Ok("Workspace created successfully");
    }
}