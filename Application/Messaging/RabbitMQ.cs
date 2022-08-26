using System.Text;
using RabbitMQ.Client;

namespace Application.Messaging;

public static class RabbitMq
{
    private static ConnectionFactory _factory;
    public static void Init()
    {
        _factory= new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST_NAME", EnvironmentVariableTarget.Process) };
        
        using (var connection = _factory.CreateConnection())
            
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "course-exam", durable: false,
                exclusive: false, autoDelete: false, arguments: null);
        }
    }

    public static void Send(string msg)
    {
        using (var connection = _factory.CreateConnection())

        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "course-exam", durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            var messageBody = Encoding.UTF8.GetBytes(msg);

            channel.BasicPublish(exchange: "", routingKey: "course-exam",
                basicProperties: null, body: messageBody);
        }
    }
}