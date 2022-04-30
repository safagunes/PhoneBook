using Polly;
using ReportService.Domain;
using ReportService.Domain.Validations;
using ReportService.Domain.WorkerServices;
namespace ReportService.API.Extentions
{
    public static class DependenciesExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            //services.AddDbContext<EFContext>(options =>
            //    //options.UseNpgsql(builder.Configuration.GetConnectionString("EFContext"), x => x.MigrationsHistoryTable("__EFMigrationsHistory".ToLower(new CultureInfo("en-US", false)), "contactdb"))
            //    options.UseInMemoryDatabase(databaseName: "reportdb"));

            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddHostedService<CreateReportWorkerService>();
            services.AddAutoMapper(typeof(MappingProfile));


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
