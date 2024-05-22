using ApiMovies.Common.Interfaces;
using ApiMovies.Models;
using ApiMovies.Models.ApiResponse;
using Newtonsoft.Json;

namespace ApiMovies.Common.Service
{
    public class MovieHttps : IMovieHttps
    {
        private readonly ISettings _appSettings;

        public MovieHttps(ISettings appSettings)
        {
            _appSettings = appSettings;

        }
        public async Task<List<TrendingApiResponse>> GetTrendingAsync()
        {
            HttpClient clientTrending = new HttpClient();

            clientTrending.DefaultRequestHeaders.Accept.Clear();

            clientTrending.DefaultRequestHeaders.Add("Trakt-Api-Key", _appSettings.TraktApiKey);

            var responseTrending = await clientTrending.GetAsync(_appSettings.UrlTrending);

            responseTrending.EnsureSuccessStatusCode();

            var responseTrendingBody = await responseTrending.Content.ReadAsStringAsync();

            var Trendings = JsonConvert.DeserializeObject<List<TrendingApiResponse>>(responseTrendingBody) ?? new List<TrendingApiResponse>();

            return Trendings;
        }

        public async Task<List<PopularApiResponse>> GetPopularAsync()
        {
            HttpClient clientTrending = new HttpClient();

            clientTrending.DefaultRequestHeaders.Accept.Clear();

            clientTrending.DefaultRequestHeaders.Add("Trakt-Api-Key", _appSettings.TraktApiKey);

            var responseTrending = await clientTrending.GetAsync(_appSettings.UrlPopular);

            responseTrending.EnsureSuccessStatusCode();

            var responseTrendingBody = await responseTrending.Content.ReadAsStringAsync();

            var Trendings = JsonConvert.DeserializeObject<List<PopularApiResponse>>(responseTrendingBody) ?? new List<PopularApiResponse>();

            return Trendings;
        }
    }
}
