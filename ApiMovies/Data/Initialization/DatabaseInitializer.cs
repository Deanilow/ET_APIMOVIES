namespace ApiMovies.Data.Initialization;
internal class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
           .InitializeAsync(cancellationToken);
    }
}