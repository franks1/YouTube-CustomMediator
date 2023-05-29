using System.Reflection;
using CustomMediator;
using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Pipeline;
using CustomMediator.Api.Behaviors;
using CustomMediator.Api.Behaviors.Commands;
using CustomMediator.Api.Behaviors.Queries;
using CustomMediator.Api.Commands;
using CustomMediator.Api.Commands.Handlers;
using CustomMediator.Api.Repositories;
using CustomMediator.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

builder.Services.AddCustomMediator(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(ICommandPipelineBehavior<>), typeof(CommandBehavior<>));
builder.Services.AddTransient(typeof(ICommandPipelineBehavior<>), typeof(CommandBehavior2<>));

builder.Services.AddTransient(typeof(IQueryPipelineBehavior<,>), typeof(QueryBehavior1<,>));
builder.Services.AddTransient(typeof(IQueryPipelineBehavior<,>), typeof(QueryBehavior2<,>));

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