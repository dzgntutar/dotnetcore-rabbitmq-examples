using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Rabbitmq.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //channel.QueueDeclare("say-hello", true, false, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("say-hello", true, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) => {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine($"Mesaj: {message} ");
            };

            Console.ReadKey();
        }

    }
}
