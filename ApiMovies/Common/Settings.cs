namespace ApiMovies.Common;
public class Settings : ISettings
{
    public string TraktApiKey { get; set; }
    public string UrlPopular { get; set; }
    public string UrlTrending { get; set; }
}