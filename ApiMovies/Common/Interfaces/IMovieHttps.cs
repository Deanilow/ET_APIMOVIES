using ApiMovies.Models;
using ApiMovies.Models.ApiResponse;

namespace ApiMovies.Common.Interfaces
{
    public interface IMovieHttps
    {
        Task<List<TrendingApiResponse>> GetTrendingAsync();
        Task<List<PopularApiResponse>> GetPopularAsync();
    }
}
