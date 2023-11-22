using System.Dynamic;
using System.Text.Json;
using CodeCollab___WorkspaceService.Interfaces;
using CodeCollab___WorkspaceService.Models;
using CodeCollab___WorkspaceService.Services;
using MongoDB.Bson.Serialization.Serializers;

namespace CodeCollab___WorkspaceService.Utils;

public class BasicMessageHandler : IMessageHandler
{
    public void HandleMessage(string message)
    {
        try
        {
            Dictionary<string, object>? jsonMessage = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
            // if (jsonMessage == null || jsonMessage == "") return; 
            
            string messageType = jsonMessage["MessageType"].ToString();
            string commandName = jsonMessage["CommandName"].ToString();
            var payload = jsonMessage["Payload"];// as IDictionary<string, object>;

            if (payload is Dictionary<string, object> payloadDict)
            {
                var Name = payloadDict["Name"];
                var OwnerId = payloadDict["OwnerId"];
            }
            else
            {
                string name = "failed...";
            }
            // WorkspaceModel workspace = new WorkspaceModel()
            // {
            //     Name = payload["Name"],
            //     OwnerId = payload["OwnerId"]
            // };
            
            WorkspaceService service = new WorkspaceService();
            // service.CreateWorkspace(workspace);
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