using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace InventoryService.Messaging
{
    public class Publisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "inventory_exchange";

        public Publisher(string hostName = "localhost")
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
        }

        public void PublishProductEvent(object product, string routingKey)
        {
            var message = JsonSerializer.Serialize(product);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
