using RabbitMQ.Client;
using ReportService.Domain.Bus;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Bus.RabbitMQ
{
    public class RabbitMQPublisher : IBusPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish<T>(T model)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(model);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.Exchange, routingKey: RabbitMQClientService.RoutingKey, basicProperties: properties, body: bodyByte);

        }
    }
}
