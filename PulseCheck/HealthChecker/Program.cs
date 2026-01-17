using HealthChecker.Workers;
using HealthChecker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseCheck.Core.Interfaces;
using PulseCheck.Infrastructure.ExternalClients;
using PulseCheck.Infrastructure.Repositories;
using PulseCheck.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args).
     ConfigureAppConfiguration((hostingContext, config) =>
     {
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
     })
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;
        services.AddDbContext<PulseCheckDBContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IAPIEndpointRepository, APIEndpointRepository>();
        services.AddScoped<IHealthCheckLogRepository, HealthCheckLogRepository>();

        services.AddSingleton<HttpClients>();

        services.AddTransient<HealthCheckProcessor>();

        services.AddHostedService<HealthCheckScheduler>();
        services.AddHttpClient();
    })
    .Build();

// Run the host — this starts the scheduler
await host.RunAsync();