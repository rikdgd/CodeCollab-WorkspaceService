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
    
    
    [HttpGet(Name = "GetWorkspace")]
    public IActionResult GetWorkspace(string workspaceId)
    {
        WorkspaceService service = new WorkspaceService();
        WorkspaceModel? workspace = service.GetWorkspaceById(workspaceId);
        
        if (workspace == null) return BadRequest("Could not find workspace with the given id.");
        return Ok(workspace);
    }
    
    
    // [HttpPost(Name = "CreateWorkspace")]
    // public IActionResult CreateWorkspace([FromBody] WorkspaceModel workspace)
    // {
    //     try 
    //     {
    //         //string workspaceJson = JsonSerializer.Serialize(workspace);
    //         MessageModel<WorkspaceModel> messageModel = new MessageModel<WorkspaceModel>(
    //             "Command", 
    //             "CreateWorkspace", 
    //             workspace
    //         );
    //
    //         string message = JsonSerializer.Serialize(messageModel);
    //         _messenger.SendMessage(message);
    //         
    //         return Ok("Workspace creation successfully added to queue.");
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest("An error occured when trying to create the workspace: \n" + ex.Message);
    //     }
    // }
}
