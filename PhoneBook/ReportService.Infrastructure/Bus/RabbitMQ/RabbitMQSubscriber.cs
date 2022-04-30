using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
    public class RabbitMQSubscriber : IBusSubscriber
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IModel _channel;
        private readonly IMediator _mediator;

        
        public RabbitMQSubscriber(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

        }

        public async Task SubscribeAsync<T>() where T : class, new()
        {
            var t = new T();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                t = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                await _mediator.Send(t);
            };
            _channel.BasicConsume(queue: RabbitMQClientService.Queue,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
