using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rabbitmq.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare("say-hello", true, false, false);


            while (true)
            {
                string message = $"Hi, Date Time is now: {DateTime.Now}";

                var messageByte = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, "say-hello", null, messageByte);

                Thread.Sleep(1000);

                Console.WriteLine($"Message sended: {message}" );

            }
        }
    }
}
