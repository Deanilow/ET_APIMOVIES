namespace ApiMovies.Routes;
public static class TrendingRoutes
{
    public static void RegisterTrendingApi(WebApplication app)
    {
        const string Api_TRENDING_COMPLETE = $"{Util.API_ROUTE}{Util.API_VERSION}{Util.TRENDING_ROUTE}";
        app.MapGet(Api_TRENDING_COMPLETE, (DBContext db) =>
        {
            var listMovies = db.Movie
            .Include(p => p.Ids)
            .Where(x => x.Popular == false).ToList();
            return Results.Ok(listMovies);
        })
        .Produces<List<Movie>?>(200)
        .WithMetadata(new SwaggerOperationAttribute(
            summary: TrendingMetadataMessages.MESSAGE_TRENDING_LIST_SUMMARY,
             description: TrendingMetadataMessages.MESSAGE_TRENDING_LIST_DESCRIPTION
             ));
    }
}