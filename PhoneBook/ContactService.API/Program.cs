using ContactService.API.Middlewares;
using ContactService.Domain;
using ContactService.Domain.Handlers;
using ContactService.Domain.Repositories;
using ContactService.Domain.Validations;
using ContactService.Infrastructure.PostgreSql;
using ContactService.Infrastructure.PostgreSql.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EFContext>(options =>
    //options.UseNpgsql(builder.Configuration.GetConnectionString("EFContext"), x => x.MigrationsHistoryTable("__EFMigrationsHistory".ToLower(new CultureInfo("en-US", false)), "contactdb"))
    options.UseInMemoryDatabase(databaseName: "contactdb")
); 

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(typeof(GetContactsHandler));

builder.Services.AddScoped<IContactRepository, PGContactRepository>();
builder.Services.AddScoped<IContactInfoRepository, PGContactInfoRepository>();

builder.Services.AddScoped<CreateContactValidator>();
builder.Services.AddScoped<CreateContactInfoValidator>();
builder.Services.AddScoped<DeleteContactValidator>();
builder.Services.AddScoped<DeleteContactInfoValidator>();
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    try
//    {
//        var context = services.GetRequiredService<EFContext>();
//        context.Database.Migrate(); // apply all migrations
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
