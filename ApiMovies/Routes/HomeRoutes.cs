using ApiMovies.Models;
using ApiMovies.Utils;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using HomeMetadataMessages = ApiMovies.Utils.Messages.EndpointMetadata.HomeEndpoint;

namespace ApiMovies.Routes
{
    public static class HomeRoutes
    {
        public static void RegisterHomeApi(WebApplication app)
        {
            const string Api_HOME_COMPLETE = $"{Util.API_ROUTE}{Util.API_VERSION}{Util.HOME_ROUTE}";
            app.MapGet(Api_HOME_COMPLETE, (DBContext db) =>
            {
                var listMovies = db.Movie.Take(10).ToList();

                listMovies.Shuffle();

                return Results.Ok(listMovies);
            })
            .Produces<List<Movie>?>(200)
            .WithMetadata(new SwaggerOperationAttribute(
                summary: HomeMetadataMessages.MESSAGE_HOME_LIST_SUMMARY,
                 description: HomeMetadataMessages.MESSAGE_HOME_LIST_DESCRIPTION
                 ));
        }
        private static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

