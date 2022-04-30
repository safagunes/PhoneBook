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
using ReportService.Domain.Services.File;
using ReportService.Domain.Validations;
using ReportService.Domain.WorkerServices;
using ReportService.Infrastructure.Bus.RabbitMQ;
using ReportService.Infrastructure.Data.EF;
using ReportService.Infrastructure.Repositories.EF;
using ReportService.Infrastructure.Services.Contact;
using ReportService.Infrastructure.Services.ExcelExport;
using ReportService.Infrastructure.Services.File;

namespace ReportService.API.Extentions
{
    public static class DependenciesExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options =>
                //options.UseNpgsql(builder.Configuration.GetConnectionString("EFContext"), x => x.MigrationsHistoryTable("__EFMigrationsHistory".ToLower(new CultureInfo("en-US", false)), "contactdb"))
                options.UseInMemoryDatabase(databaseName: "reportdb"));

            var dd = configuration.GetConnectionString("RabbitMQ");
            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

            services.AddSingleton<RabbitMQClientService>();
            services.AddSingleton<IBusPublisher, RabbitMQPublisher>();
            services.AddSingleton<IBusSubscriber, RabbitMQSubscriber>();

            services.AddHostedService<CreateReportWorkerService>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMediatR(typeof(CreateReportHandler), typeof(GetReportHandler), typeof(GetReportsHandler), typeof(ProcessReportHandler));

            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IExcelExportService, ClosedXMLExcelExportService>();
            services.AddScoped<IFileService, LocalFileService>();

            services.AddScoped<IReportDetailRepository, EFReportDetailRepository>();
            services.AddScoped<IReportRepository, EFReportRepository>();


            services.AddScoped<CreateReportValidator>();


            services.AddHttpClient("contactservice", c =>
            {
                c.BaseAddress = new Uri(configuration["Services:Contact"]);
                c.Timeout = TimeSpan.FromMinutes(1);

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));


            return services;
        }
    }
}
