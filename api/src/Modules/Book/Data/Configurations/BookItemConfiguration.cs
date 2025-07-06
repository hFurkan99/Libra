namespace Book.Data.Configurations;
public class BookItemConfiguration : IEntityTypeConfiguration<BookItem>
{
    public void Configure(EntityTypeBuilder<BookItem> builder)
    {
        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.CatalogBookId)
            .IsRequired();

        builder.Property(bi => bi.Barcode)
            .HasMaxLength(50);

        builder.Property(bi => bi.Location)
            .HasMaxLength(10);

        builder.Property(bi => bi.ConditionNote)
            .HasMaxLength(250);

        builder.Property(bi => bi.Status)
            .HasConversion<string>()
            .HasDefaultValue(BookStatus.Available);
    }
}
