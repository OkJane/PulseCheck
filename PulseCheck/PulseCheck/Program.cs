using Microsoft.EntityFrameworkCore;
using PulseCheck.Core.Interfaces;
using PulseCheck.Infrastructure.Data;
using PulseCheck.Infrastructure.ExternalClients;
using PulseCheck.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAPIEndpointRepository, APIEndpointRepository>();
builder.Services.AddScoped<IHealthCheckLogRepository, HealthCheckLogRepository>();
builder.Services.AddSingleton<HttpClients>();
builder.Services.AddDbContext<PulseCheckDBContext>(x => x.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddHttpClient();

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
