namespace AssessmentSystem.Service.API.Services.RabbitMQService
{
    public interface IRabbitMqPublisher
    {
        public void Publish(string token, string queueName);

    }
}
