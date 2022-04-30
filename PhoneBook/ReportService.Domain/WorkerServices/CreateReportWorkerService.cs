using MediatR;
using Microsoft.Extensions.Hosting;
using ReportService.Domain.Bus;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.WorkerServices
{
    public class CreateReportWorkerService : BackgroundService
    {
        private readonly IBusSubscriber _busSubscriber;
        
        public CreateReportWorkerService(IBusSubscriber busSubscriber)
        {
            _busSubscriber = busSubscriber;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
                await _busSubscriber.SubscribeAsync<ProcessReport>();
            }
        }
    }
}
