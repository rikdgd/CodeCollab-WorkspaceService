using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeCollab___WorkspaceService.Utils;

public class BasicMessageHandler : IMessageHandler
{
    public bool HandleMessage(string message)
    {
        try
        {
            var jsonMessage = JObject.Parse(message);
            
            var messageType = jsonMessage?["MessageType"].ToString();
            var commandName = jsonMessage?["CommandName"].ToString();
            var payload = jsonMessage["Payload"] as JObject;
            
            WorkspaceModel workspace = new WorkspaceModel()
            {
                Name = (string)payload["Name"],
                OwnerId = (int)payload["OwnerId"]
            };
            
            WorkspaceService service = new WorkspaceService();
            service.CreateWorkspace(workspace);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private enum MessageType
    {
        Command,
        Status,
        Error
    }
}