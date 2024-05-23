namespace ApiMovies.Routes;
public static class PopularRoutes
{
    public static void RegisterPopularApi(WebApplication app)
    {
        const string Api_POPULAR_COMPLETE = $"{Util.API_ROUTE}{Util.API_VERSION}{Util.POPULAR_ROUTE}";
        app.MapGet(Api_POPULAR_COMPLETE, (DBContext db) =>
        {
            var listMovies = db.Movie
            .Include(p => p.Ids)
            .Where(x => x.Popular == true).ToList();
            return Results.Ok(listMovies);
        })
        .Produces<List<Movie>?>(200)
        .WithMetadata(new SwaggerOperationAttribute(
            summary: PopularMetadataMessages.MESSAGE_POPULAR_LIST_SUMMARY,
             description: PopularMetadataMessages.MESSAGE_POPULAR_LIST_DESCRIPTION
             ));
    }
}