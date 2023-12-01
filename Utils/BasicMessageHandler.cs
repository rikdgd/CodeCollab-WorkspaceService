using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CarrotMQ;
using CodeCollab___WorkspaceService.Interfaces;

namespace CodeCollab___WorkspaceService.Utils;

public class BasicMessageHandler : IMessageHandler
{
    public IWorkspaceService service { get; private set; }

    public BasicMessageHandler(IWorkspaceService service)
    {
        this.service = service;
    }

    public void HandleMessage(string message)
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
            
            // WorkspaceService service = new WorkspaceService();
            this.service.CreateWorkspace(workspace);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private enum MessageType
    {
        Command,
        Status,
        Error
    }
}