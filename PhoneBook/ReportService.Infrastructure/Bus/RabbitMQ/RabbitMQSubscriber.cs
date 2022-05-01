using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RabbitMQSubscriber> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IModel _channel;        
        private readonly IServiceProvider _serviceProvider;

        
        public RabbitMQSubscriber(RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider, ILogger<RabbitMQSubscriber> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _rabbitMQClientService = rabbitMQClientService;
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

        }

        public async Task SubscribeAsync<T>()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: RabbitMQClientService.Queue,
                                 autoAck: false,
                                 consumer: consumer);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    await Task.Delay(5000);
                    var t = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<IMediator>();
                        await mediator.Send(t);
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"RabbitMQ Subscriber Exception:{ex.Message}");
                }
                
            };
        }
    }
}
