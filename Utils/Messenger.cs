using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CodeCollab___WorkspaceService.Utils;

public class Messenger : IDisposable
{
    public string ExchangeName { get; private set; }
    public string QueueName { get; private set; }
    
    private List<string>? messageStorage { get; set; }
    private EventingBasicConsumer? consumer { get; set; }
    private ConnectionFactory factory { get; set; }
    private string? consumerTag { get; set; }
    private IConnection connection { get; set; }
    private IModel channel { get; set; }
    private IBasicProperties publishProps { get; set; }

    /// <summary>
    /// Create a new messenger to interact with a RabbitMQ message bus.
    /// </summary>
    /// <param name="hostName">The name of the host, this should be the URI that is used to reach the message bus.</param>
    /// <param name="appName">The name of the current application.</param>
    /// <param name="exchangeName">The name of the exchange this messenger should be connected to.</param>
    /// <param name="queueName">The name of the message queue this messenger should be connected to.</param>
    /// <param name="isConsumer">Specifies whether this messenger can only send or also receive messages.</param>
    /// <param name="autoDeclare">Whether ot not the messenger should automatically declare unexisting exchanges and queues.</param>
    public Messenger(string hostName, string appName, string exchangeName, string queueName, bool isConsumer = true, bool autoDeclare = true)
    {
        this.ExchangeName = exchangeName;
        this.QueueName = queueName;
        this.factory = new ConnectionFactory()
        { 
            HostName = hostName, 
            ClientProvidedName = appName 
        };
        
        this.connection = this.factory.CreateConnection();
        this.channel = this.connection.CreateModel();

        if (autoDeclare)
        {
            this.channel.ExchangeDeclare(
                exchange: this.ExchangeName,
                type: ExchangeType.Direct
            );
            
            this.channel.QueueDeclare(
                queue: this.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            
            this.channel.QueueBind(
                queue: this.QueueName,
                exchange: this.ExchangeName,
                routingKey: this.QueueName
            );
        }

        this.publishProps = this.channel.CreateBasicProperties();
        this.publishProps.Persistent = true;

        if (isConsumer)
        {
            this.channel.BasicQos(
                prefetchSize: 0, 
                prefetchCount: 1, 
                global: false
            );
            
            this.messageStorage = new List<string>();
            this.consumer = new EventingBasicConsumer(this.channel);
            this.consumer.Received += (sender, args) =>
            {
                byte[] messageBody = args.Body.ToArray();
                string receivedMessage = Encoding.UTF8.GetString(messageBody);

                this.messageStorage.Add(receivedMessage);
                
                // ToDo: Acknowledge the message ONLY when handling it went successful.
                this.channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
            };
            
            this.consumerTag = channel.BasicConsume(
                queue: this.QueueName, 
                autoAck: false, 
                consumer: this.consumer
            );
        }
    }
    
    public void Dispose()
    {
        // Close the connection to the message bus.
        if (this.consumer != null) 
        {
            channel.BasicCancel(this.consumerTag);
        }
        this.channel.Close();
        this.connection.Close();
    }
    
    /// <summary>
    /// Send a message to the message bus. 
    /// </summary>
    /// <param name="message">The message that should be send.</param>
    public void SendMessage(string message)
    {
        byte[] messageBody = Encoding.UTF8.GetBytes(message);
        
        this.channel.BasicPublish(
            exchange: this.ExchangeName, 
            routingKey: this.QueueName,
            basicProperties: this.publishProps,
            body: messageBody
        );
    }

    /// <summary>
    /// Send a message to the message bus.
    /// </summary>
    /// <param name="message">The message that should be send.</param>
    public void SendMessage(byte[] message)
    {
        this.channel.BasicPublish(
            exchange: this.ExchangeName, 
            routingKey: this.QueueName,
            basicProperties: this.publishProps,
            body: message
        );
    }

    /// <summary>
    /// Reads the messages from the message bus.
    /// </summary>
    /// <returns>A list with all the received messages.</returns>
    public List<string>? ReadMessages()
    {
        if (this.messageStorage != null) 
        {
            return this.messageStorage;
        }
        
        return null;
    }

    /// <summary>
    /// Reads the last message from the message bus.
    /// </summary>
    /// <returns>A string containing the final message that was received. </returns>
    public string? ReadLastMessage()
    {
        if (this.messageStorage == null) return null;
        
        return this.messageStorage[0];
    }
    
    public string? ReadAndDestroyMessage()
    {
        if (this.messageStorage == null) return null;
        
        string message =  this.messageStorage[0];
        messageStorage.RemoveAt(0);
        
        return message;
    }

    public List<string>? readAndDestroyMessages()
    {
        if (this.messageStorage == null) return null;
        
        var messages = this.messageStorage;
        messageStorage.Clear();
        
        return messages;
    }
}

