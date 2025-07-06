namespace Book.Data.Configurations;
public class BookStockConfiguration : IEntityTypeConfiguration<BookStock>
{
    public void Configure(EntityTypeBuilder<BookStock> builder)
    {
        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.CatalogBookId)
            .IsRequired();

        builder.Property(bs => bs.Total)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(bs => bs.Available)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
