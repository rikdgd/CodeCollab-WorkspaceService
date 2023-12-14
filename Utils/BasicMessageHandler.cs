using Newtonsoft.Json.Linq;
using RabbitMessenger.Interfaces;

using CodeCollab___WorkspaceService.Models;
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
            
            if (messageType == "Command")
            {
                if (commandName == "CreateWorkspace") CreateWorkspace(jsonMessage);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void CreateWorkspace(JObject? messageData)
    {
        try
        {
            var payload = messageData["Payload"] as JObject;

            WorkspaceModel workspace = new WorkspaceModel()
            {
                Name = (string)payload["Name"],
                OwnerId = (string)payload["OwnerId"]
            };

            this.service.CreateWorkspace(workspace);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}