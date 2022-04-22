using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

class Program
{
    static Task Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        var connection = factory.CreateConnection();

        var x = string.Empty;

        while (x != "X")
        {
            Console.WriteLine("Press any key to continue");
            x = Console.ReadLine()?.ToUpperInvariant();

            if (x != "X")
            {
                Console.Write("Customer Name: ");
                var name = Console.ReadLine();
                Console.Write("Email: ");
                var email = Console.ReadLine();

                var customer = new
                {
                    id = Guid.NewGuid().ToString(),
                    name = name,
                    email = email
                };

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "customer",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var customerPayload = JsonSerializer.Serialize(customer);
                    var body = Encoding.UTF8.GetBytes(customerPayload);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "customer",
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }

        return Task.CompletedTask;
    }
}

