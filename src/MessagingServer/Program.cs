using System.Text.Json;
using System.Text.Json.Serialization;
using MessagingServer.Services;
using MessagingServer.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MessagingServer;

var builder = WebApplication.CreateBuilder(args);

// Get Config
var appSettings = new AppConfig();
builder.Configuration
    .GetSection("AppConfig")
    .Bind(appSettings);

// Register Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new ProviderModule(appSettings)));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

// Learn more about configuring Swagger/OpenAPI at httpß://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();

app.Run();