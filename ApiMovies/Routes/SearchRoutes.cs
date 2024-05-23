namespace ApiMovies.Routes;
public static class SearchRoutes
{
    public static void RegisterSearchApi(WebApplication app)
    {
        const string Api_SEARCH_COMPLETE = $"{Util.API_ROUTE}{Util.API_VERSION}{Util.SEARCH_ROUTE}";
        app.MapGet($"{Api_SEARCH_COMPLETE}/movie/{{name}}", async (string name, DBContext db) =>
        {
            var movie = await db.Movie
            .Where(x => x.Title!.ToUpper().Contains(name.Trim().ToUpper())).FirstOrDefaultAsync();

            if (movie is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(movie);
        })
        .Produces<List<Movie>?>(200)
        .WithMetadata(new SwaggerOperationAttribute(
            summary: SearchMetadataMessages.MESSAGE_SEARCH_LIST_SUMMARY,
             description: SearchMetadataMessages.MESSAGE_SEARCH_LIST_DESCRIPTION
             ));
    }
}