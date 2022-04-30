using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Bus.RabbitMQ
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string Exchange = "ReportDirectExchange";
        public static string RoutingKey = "report-route";
        public static string Queue = "report-queue";

        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();


            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(Exchange, type: "direct", true, false);

            _channel.QueueDeclare(Queue, true, false, false, null);


            _channel.QueueBind(exchange: Exchange, queue: Queue, routingKey: RoutingKey);

            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");


            return _channel;

        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");

        }
    }
}
