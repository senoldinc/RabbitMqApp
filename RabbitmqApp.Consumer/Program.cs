using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();
        channel.QueueDeclare(
               queue: "customer",
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null
           );
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) => {
            var body = ea.Body.ToArray();
            var data = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Getting customer: {data}");
        };

        channel.BasicConsume(queue: "customer", autoAck: true, consumer: consumer);

        Console.ReadLine();
    }
}