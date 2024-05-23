namespace ApiMovies.Routes;
public static class MovieRoutes
{
    public static void RegisterMovieApi(WebApplication app)
    {
        const string Api_MOVIE_COMPLETE = $"{Util.API_ROUTE}{Util.API_VERSION}{Util.MOVIES_ROUTE}";
        app.MapGet(Api_MOVIE_COMPLETE, (DBContext db) =>
        {
            var listMovies = db.Movie
            .Include(p => p.Ids).ToList();
            return Results.Ok(listMovies);
        })
        .Produces<List<Movie>?>(200)
        .WithMetadata(new SwaggerOperationAttribute(
            summary: MovieMetadataMessages.MESSAGE_MOVIE_LIST_SUMMARY,
             description: MovieMetadataMessages.MESSAGE_MOVIE_LIST_DESCRIPTION
             ));
    }
}