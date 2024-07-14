using AssessmentSystem.Service.BusinessLogic;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace AssessmentSystem.Service.API.Services.RabbitMQService
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private IConnection _connection;
        private IModel _channel;
        private IConfiguration _configuration;
        private ScoreCalculationService _scoreCalculationService;

        public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger, ScoreCalculationService scoreCalculationService, IConfiguration configuration)
        {
            _logger = logger;
            _scoreCalculationService = scoreCalculationService;
            _configuration = configuration;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("RabbitMq:HostName"),
                UserName = _configuration.GetValue<string>("RabbitMq:UserName"),
                Password = _configuration.GetValue<string>("RabbitMq:Password")
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            string mainQueueName = _configuration.GetValue<string>("RabbitMqQueue:ExamScoringQueue");
            string deadLetterExchangeName = _configuration.GetValue<string>("RabbitMqQueue:DeadLetterExchangeName");
            string deadLetterQueueName = _configuration.GetValue<string>("RabbitMqQueue:DeadLetterQueueName");

            _channel.ExchangeDeclare(exchange: deadLetterExchangeName, type: ExchangeType.Direct);

            _channel.QueueDeclare(queue: deadLetterQueueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _channel.QueueBind(queue: deadLetterQueueName,
                               exchange: deadLetterExchangeName,
                               routingKey: deadLetterQueueName);

            var arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", deadLetterExchangeName },
            { "x-dead-letter-routing-key", deadLetterQueueName }
        };

            _channel.QueueDeclare(queue: mainQueueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: arguments);

            _channel.BasicQos(0, 1, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string mainQueueName = _configuration.GetValue<string>("RabbitMqQueue:ExamScoringQueue");

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    message = CleanMessage(message);
                    _logger.LogInformation($"Received message: {message}");
                    Console.WriteLine($"Processing: UserToken={message}");
                    CalculateScore(message);

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message.");
                    _channel.BasicNack(ea.DeliveryTag, false, false); // Send message to DLX
                }
            };

            _channel.BasicConsume(queue: mainQueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private void CalculateScore(string message)
        {
            int totalScore = 0;
            int candidateScore = 0;
            AnswerManager answerManager = new();
            List<Answer> answers = answerManager.GetAnswers(message);
            foreach (var answer in answers)
            {
                ChoiceManager choiceManager = new();
                CorrectChoiceDto correctChoice = choiceManager.GetCorrectChoice(answer.QuestionId);
                if (correctChoice != null)
                {
                    totalScore = totalScore + correctChoice.Points;

                    if (correctChoice.AnswerType == AnswerTypes.MultipleChoice)
                    {
                        if (correctChoice.Answer == answer.UserAnswer)
                        {
                            candidateScore = candidateScore + correctChoice.Points;
                        }
                    }
                    else
                    {
                        candidateScore += _scoreCalculationService.AssignScore(correctChoice.Answer, answer.UserAnswer, correctChoice.Points);
                    }
                }
            }
            CandidateScore newCandidateScore = new()
            {
                CreatedBy = message,
                Rank = _scoreCalculationService.CalculateRank(candidateScore, totalScore),
                ScoreEarned = candidateScore,
                TotalScore = totalScore
            };
            CandidateScoreManager candidateScoreManager = new();
            newCandidateScore = candidateScoreManager.InsertCandidateScore(newCandidateScore);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

        private string CleanMessage(string message)
        {
            // Removing extra escape characters or unnecessary backslashes
            message = message.Replace("\\\"", "\"");  // Removes escaped quotes
            message = message.Replace("\\\\", "\\");  // Removes double backslashes
            message = message.Trim('\"');             // Removes surrounding quotes if any
            return message;
        }
    }
}
