using ContactService.API.Middlewares;
using ContactService.Domain;
using ContactService.Domain.Validations;

var builder = WebApplication.CreateBuilder(args);

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
