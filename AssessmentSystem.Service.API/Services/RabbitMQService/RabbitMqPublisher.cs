using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace AssessmentSystem.Service.API.Services.RabbitMQService
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConfiguration _configuration;
        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Publish(string token, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("RabbitMq:HostName"),
                UserName = _configuration.GetValue<string>("RabbitMq:UserName"),
                Password = _configuration.GetValue<string>("RabbitMq:Password")
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string deadLetterExchangeName = _configuration.GetValue<string>("RabbitMqQueue:DeadLetterExchangeName");
                string deadLetterQueueName = _configuration.GetValue<string>("RabbitMqQueue:DeadLetterQueueName");

                channel.ExchangeDeclare(exchange: deadLetterExchangeName, type: ExchangeType.Direct);

                channel.QueueDeclare(queue: deadLetterQueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueBind(queue: deadLetterQueueName,
                                  exchange: deadLetterExchangeName,
                                  routingKey: deadLetterQueueName);

                var arguments = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", deadLetterExchangeName },
                { "x-dead-letter-routing-key", deadLetterQueueName }
            };

                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: arguments);

                var message = JsonSerializer.Serialize(token);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Scoring request sent for user: {message}");
            }
        }
    }
}
