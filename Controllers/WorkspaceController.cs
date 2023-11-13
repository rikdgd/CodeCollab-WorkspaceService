using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Utils;
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
    
    [HttpGet(Name = "GetWorkspaceFiles")]
    public IActionResult GetWorkspaceFiles(int workspaceId) {
        Messenger messenger = new Messenger("localhost", "code-files", true);
        messenger.SendMessage("GET files FOR workspace WHERE id = " + workspaceId);
        List<string>? messages = messenger.ReadMessages();
        return Ok("success");
    }
}