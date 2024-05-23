namespace ApiMovies.Data.Configs;
public class IdsConfig : IEntityTypeConfiguration<Ids>
{
    public void Configure(EntityTypeBuilder<Ids> ids)
    {
        ids.ToTable("Ids");
        ids.HasKey(p => p.Id);
        ids.Property(p => p.Id).ValueGeneratedOnAdd();
        ids.Property(p => p.Slug).IsRequired(false).HasMaxLength(150);
        ids.Property(p => p.Imdb).IsRequired(false).HasMaxLength(150);
    }
}