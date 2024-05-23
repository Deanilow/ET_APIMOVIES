namespace ApiMovies.Models.ApiResponse;
public class TrendingApiResponse
{
    public int Watchers { get; set; }
    public Movie Movie { get; set; } = new Movie();
}