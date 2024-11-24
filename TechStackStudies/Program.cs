using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TechStackStudies.Configuration;
using TechStackStudies.Infrastructure.Data;
using Wolverine;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// DbContext Registration
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Serilog Registration
builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Register Wolverine
builder.Host.UseWolverine(opts =>
{
    opts.Policies.MessageExecutionLogLevel(LogLevel.None);
    opts.Policies.MessageSuccessLogLevel(LogLevel.Debug);
});

// Validators Service Registration
builder.Services.AddValidators();

// Repositories Service Registration
builder.Services.AddRepositories();

var app = builder.Build();

app.UseCors(c =>
{
    c.WithOrigins("http://localhost:5173");
    c.AllowAnyHeader();
    c.AllowAnyMethod();
});

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
