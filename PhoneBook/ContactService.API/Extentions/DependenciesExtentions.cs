using ContactService.Domain;
using ContactService.Domain.Handlers;
using ContactService.Domain.Repositories;
using ContactService.Domain.Validations;
using ContactService.Infrastructure.Repositories.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ContactService.API.Extentions
{
    public static class DependenciesExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("EFContext"), x => x.MigrationsHistoryTable("__EFMigrationsHistory".ToLower(new CultureInfo("en-US", false)), "contactdb"))
                //options.UseInMemoryDatabase(databaseName: "contactdb")
                );

            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMediatR(typeof(GetContactsHandler));
            services.AddAutoMapper(typeof(MappingProfile));


            services.AddScoped<IContactRepository, EFContactRepository>();
            services.AddScoped<IContactInfoRepository, EFContactInfoRepository>();


            services.AddScoped<CreateContactValidator>();
            services.AddScoped<CreateContactInfoValidator>();
            services.AddScoped<DeleteContactValidator>();
            services.AddScoped<DeleteContactInfoValidator>();


            return services;
        }
    }
}
