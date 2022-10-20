namespace Cryptocop.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        private readonly string _exchangeName;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        
        public QueueService(IConfiguration configuration)
        {
                var messageBrokerSection = configuration.GetSection("MessageBroker");
                
                var host = messageBrokerSection
                    .GetValue<string>("Host");
                _exchangeName = messageBrokerSection
                    .GetValue<string>("Exchange");
                
                var factory = new ConnectionFactory()
                {
                    HostName = host
                };
                
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
        }
        
        public void PublishMessage(string routingKey, object body)
        {
            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            _channel.BasicPublish(
                _exchangeName, 
                routingKey, 
                null, 
                jsonBytes);
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}