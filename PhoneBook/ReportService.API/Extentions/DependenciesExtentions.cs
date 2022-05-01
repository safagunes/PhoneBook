using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using RabbitMQ.Client;
using ReportService.Domain;
using ReportService.Domain.Bus;
using ReportService.Domain.Handlers;
using ReportService.Domain.Repositories;
using ReportService.Domain.Services;
using ReportService.Domain.Services.ExcelExport;
using ReportService.Domain.Validations;
using ReportService.Domain.WorkerServices;
using ReportService.Infrastructure.Bus.RabbitMQ;
using ReportService.Infrastructure.Data.EF;
using ReportService.Infrastructure.Repositories.EF;
using ReportService.Infrastructure.Services.Contact;
using ReportService.Infrastructure.Services.ExcelExport;
using System.Globalization;

namespace ReportService.API.Extentions
{
    public static class DependenciesExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options =>
                //options.UseNpgsql(configuration.GetConnectionString("EFContext"), x => x.MigrationsHistoryTable("__EFMigrationsHistory".ToLower(new CultureInfo("en-US", false)), "public"))
                options.UseInMemoryDatabase(databaseName: "reportdb")
                );

            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

            services.AddSingleton<RabbitMQClientService>();
            services.AddSingleton<IBusPublisher, RabbitMQPublisher>();
            services.AddSingleton<IBusSubscriber, RabbitMQSubscriber>();

            services.AddHostedService<CreateReportWorkerService>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMediatR(typeof(CreateReportHandler), typeof(GetReportHandler), typeof(GetReportsHandler), typeof(ProcessReportHandler));

            services.AddSingleton<IContactService, ContactService>();
            services.AddScoped<IExcelExportService, ClosedXMLExcelExportService>();

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IReportDetailRepository, EFReportDetailRepository>();
            services.AddScoped<IReportRepository, EFReportRepository>();


            services.AddScoped<CreateReportValidator>();


            services.AddHttpClient("contactservice", c =>
            {
                c.BaseAddress = new Uri(configuration["Services:Contact"]);
                c.Timeout = TimeSpan.FromMinutes(1);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            })
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));


            return services;
        }
    }
}
