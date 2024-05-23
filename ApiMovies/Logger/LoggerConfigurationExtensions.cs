namespace ApiMovies.Logger;
public static class LoggerConfigurationExtensions
{
    public static void UseSerilogConfig(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .ConfigureBaseLogging()
            .CreateLogger();

        builder.Host.UseSerilog(Log.Logger);
    }
    public static LoggerConfiguration ConfigureBaseLogging(this LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId();


        return loggerConfiguration;
    }
}