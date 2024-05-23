namespace ApiMovies.Data.Configs;
public class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> Movie)
    {
        Movie.ToTable("Movie");
        Movie.HasKey(p => p.Id);
        Movie.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}