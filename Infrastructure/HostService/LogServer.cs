using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;
using MessageProcessingService_OCSS.Domain.Entities;
using System.Net.Http.Json;
using Newtonsoft.Json;
using MessageProcessingService_OCSS.Infrastructure.EFCore;

namespace MessageProcessingService_OCSS.Infrastructure.HostService
{
    public class LogServer : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IModel _channel;
        private readonly IConnection _connection;

        public LogServer(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetSection("RabbitMq")["HostName"],
                Port = Convert.ToInt32(configuration.GetSection("RabbitMq")["Port"]),
                UserName = configuration.GetSection("RabbitMq")["Username"],
                Password = configuration.GetSection("RabbitMq")["Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "Logs",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var log = JsonConvert.DeserializeObject<Log>(message);
                if(log == null) return;
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<LogServiceOcssContext>();
                await dbContext.Logs.AddAsync(log, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            };

            // 开始消费队列中的消息
            _channel.BasicConsume(queue: "Logs", autoAck: true, consumer: consumer);

            // 持续保持任务运行
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
