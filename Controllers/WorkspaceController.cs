using System.Text.Json;
using System.Text.Json.Serialization;
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
        try 
        {
            string workspaceData = JsonSerializer.Serialize(workspace);
            
            using (Messenger messenger = new Messenger("localhost", "WorkspaceService", "test-exchange", "test-queue", isConsumer: false))
            {
                messenger.SendMessage("CREATE workspace FROM: " + workspaceData);
            }
            
            return Ok("Workspace created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    

}
