namespace ApiMovies.Common.Interfaces;
public interface ISettings
{
    string TraktApiKey { get; set; }
    string UrlTrending { get; set; }
    string UrlPopular { get; set; }
}