using DataAccess.Context;
using Domain;
using Polly;
using Polly.CircuitBreaker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService
{
    public class Consumer : BackgroundService
    {
        private readonly ILogger<Consumer> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private RabbitMQ.Client.IModel _channel;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public Consumer(ILogger<Consumer> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("inventory_exchange", ExchangeType.Direct);

            _channel.QueueDeclare("product_created_queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("product_updated_queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("product_deleted_queue", durable: true, exclusive: false, autoDelete: false);

            _channel.QueueBind("product_created_queue", "inventory_exchange", "product.created");
            _channel.QueueBind("product_updated_queue", "inventory_exchange", "product.updated");
            _channel.QueueBind("product_deleted_queue", "inventory_exchange", "product.deleted");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RegisterConsumer("product_created_queue", "Created");
            RegisterConsumer("product_updated_queue", "Updated");
            RegisterConsumer("product_deleted_queue", "Deleted");
        }

        private void RegisterConsumer(string queueName, string eventType)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, args) =>
            {
                await _circuitBreakerPolicy.ExecuteAsync(async () =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var product = JsonSerializer.Deserialize<ProductMessage>(message);

                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

                    db.InventoryEvents.Add(new InventoryEvent
                    {
                        EventType = eventType,
                        ProductId = product.Id.ToString(),
                        ProductName = product.Name,
                        Timestamp = DateTime.UtcNow
                    });

                    await db.SaveChangesAsync();
                    _channel.BasicAck(args.DeliveryTag, false);
                });
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }

    public class ProductMessage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
