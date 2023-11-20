using System.Text.Json;
using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;

namespace CodeCollab___WorkspaceService.Utils;

public class BasicMessageHandler : IMessageHandler
{
    public void HandleMessage(string message)
    {
        try
        {
            dynamic? jsonMessage = JsonSerializer.Deserialize<dynamic>(message);
            if (jsonMessage == null) return; 
            
            var messageType = jsonMessage.MessageType;
            var commandName = jsonMessage.CommandName;
            var payload = jsonMessage.Payload;
            
            WorkspaceModel workspace = new WorkspaceModel()
            {
                Name = payload.Name,
                OwnerId = payload.OwnerId
            };

            WorkspaceService service = new WorkspaceService();
            service.CreateWorkspace(workspace);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private enum MessageType
    {
        Command,
        Status,
        Error
    }
}