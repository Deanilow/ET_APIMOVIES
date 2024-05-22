using ApiMovies.Common.Interfaces;

namespace ApiMovies.Data.Initialization
{
    internal class ApplicationDbSeeder
    {
        private readonly ILogger<ApplicationDbSeeder> _logger;
        private readonly IMovieHttps _IMovieHttps;

        public ApplicationDbSeeder(ILogger<ApplicationDbSeeder> logger, IMovieHttps IMovieHttps)
        {
            _IMovieHttps = IMovieHttps;
            _logger = logger;
        }

        public async Task SeedDatabaseAsync(DBContext dbContext, CancellationToken cancellationToken)
        {
            await SeedDataTrendingPopularAsync(dbContext);
        }
        private async Task SeedDataTrendingPopularAsync(DBContext dbContext)
        {
            if (!dbContext.Movie.Any())
            {
                var Trendings = await _IMovieHttps.GetTrendingAsync();

                foreach (var item in Trendings)
                {
                    item.Movie.Watchers = item.Watchers;

                    await dbContext.Movie.AddAsync(item.Movie);

                    _logger.LogInformation($"Insert Trending: Title: {item.Movie.Title}, Year: {item.Movie.Year}, Watchers: {item.Watchers}");
                }

                var Populars = await _IMovieHttps.GetPopularAsync();

                foreach (var item in Populars)
                {
                    item.Popular = true;

                    await dbContext.Movie.AddAsync(item);
                    _logger.LogInformation($"Insert Popular: Title: {item.Title}, Year: {item.Year}");
                }

                await dbContext.SaveChangesAsync();
            }
        }

    }
}
