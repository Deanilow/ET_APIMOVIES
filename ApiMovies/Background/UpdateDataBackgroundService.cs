namespace ApiMovies.Background;
public class UpdateDataBackgroundService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly ILogger<UpdateDataBackgroundService> _logger;
    private readonly IMovieHttps _IMovieHttps;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    public UpdateDataBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<UpdateDataBackgroundService> logger, IMovieHttps IMovieHttps)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _IMovieHttps = IMovieHttps;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background Service is starting.");

        // Inicializa el temporizador para ejecutar `DoWorkAsync` cada hora

        _timer = new Timer(async state => await DoWorkAsync(state), null, TimeSpan.Zero, TimeSpan.FromHours(1));

        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background Service is stopping.");

        _timer?.Dispose();

        return Task.CompletedTask;
    }

    private async Task DoWorkAsync(object state)
    {
        _logger.LogInformation("Background Service is working.");

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();

            var listIdsToDelete = await _dbContext.Ids.ToListAsync();
            _dbContext.Ids.RemoveRange(listIdsToDelete);
            _dbContext.SaveChanges();

            var listMovieToDelete = await _dbContext.Movie.ToListAsync();
            _dbContext.Movie.RemoveRange(listMovieToDelete);
            _dbContext.SaveChanges();

            var Trendings = await _IMovieHttps.GetTrendingAsync();

            foreach (var item in Trendings)
            {
                item.Movie.Watchers = item.Watchers;

                await _dbContext.Movie.AddAsync(item.Movie);

                //_logger.LogInformation($"Background Service Insert Trending: Title: {item.Movie.Title}, Year: {item.Movie.Year}, Watchers: {item.Watchers}");
            }

            var Populars = await _IMovieHttps.GetPopularAsync();

            foreach (var item in Populars)
            {
                item.Popular = true;

                await _dbContext.Movie.AddAsync(item);

                //_logger.LogInformation($"Background Service Insert Popular: Title: {item.Title}, Year: {item.Year}");
            }

            await _dbContext.SaveChangesAsync();
        }
    }
    public void Dispose()
    {
        _timer?.Dispose();
    }
}