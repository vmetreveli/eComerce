using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Discount.API;

public static class RegisterSerilogServices
{
    private static readonly IDictionary<string, ColumnWriterBase> _columnWriters =
        new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar)},
            {"raise_date", new TimestampColumnWriter()},
            {"exception", new ExceptionColumnWriter()}
        };

    /// <summary>
    ///     Register the Serilog service with a custom configuration.
    /// </summary>
    private static IServiceCollection AddSerilogServices(this IServiceCollection services,
        LoggerConfiguration configuration)
    {
        Log.Logger = configuration.CreateLogger();
        // SelfLog.Enable(msg => Debug.WriteLine(msg));
        // SelfLog.Enable(Console.Error);
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
        return services.AddSingleton(Log.Logger);
    }


    public static IServiceCollection AddSerilogServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddSerilogServices(
            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
        );
}