using System.Text.Json;
using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;

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
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}