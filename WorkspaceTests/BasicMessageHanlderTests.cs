using CodeCollab___WorkspaceService.Utils;
using WorkspaceTests.Mock;


namespace WorkspaceTests;

public class BasicMessageHanlderTests
{
    [Fact]
    public void CreateWorkspaceTest()
    {
        // Arrange
        WorkspaceServiceMock service = new WorkspaceServiceMock();
        BasicMessageHandler messageHandler = new BasicMessageHandler(service);
        string correctCreateMessage = "{\"MessageType\":\"Command\",\"CommandName\":\"CreateWorkspace\",\"Payload\":{\"Name\":\"test-workspace\",\"OwnerId\":22}}";
        string faultyCreateMessage = "{\"MessageType\":\"Command\",\"CommandName\":true,\"Payload\":{\"Name\":22,\"OwnerId\":\"Hello world\"}}";
        string notCommandMessage =
            "{\"MessageType\":\"not-command\",\"CommandName\":\"CreateWorkspace\",\"Payload\":{\"Name\":\"test-workspace\",\"OwnerId\":22}}";
        

        // Act
        messageHandler.HandleMessage(correctCreateMessage); // at index 0
        messageHandler.HandleMessage(faultyCreateMessage);
        messageHandler.HandleMessage(notCommandMessage);

        // Assert
        Assert.Single(service.workspaces);
    }
}