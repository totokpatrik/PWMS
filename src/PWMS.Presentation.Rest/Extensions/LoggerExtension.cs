using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Globalization;
using System.Reflection;

namespace PWMS.Presentation.Rest.Extensions;

public static class LoggerExtension
{
    public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var serviceName = Assembly.GetCallingAssembly().GetName().Name!;

        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty("PWMS", serviceName, true)
            .Enrich.FromLogContext()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();

        Log.Logger = logger;

        var loggerFactory = new LoggerFactory();
        loggerFactory.AddSerilog(logger);
        services.AddSingleton<ILoggerFactory>(loggerFactory);

        return services;
    }
}
