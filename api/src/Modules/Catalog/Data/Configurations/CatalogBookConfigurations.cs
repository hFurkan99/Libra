namespace Catalog.Data.Configurations;
public class CatalogBookConfigurations : IEntityTypeConfiguration<CatalogBook>
{
    public void Configure(EntityTypeBuilder<CatalogBook> builder)
    {
        // Key
        builder.HasKey(b => b.Id);

        // Properties
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        // Value Object: Isbn
        builder.OwnsOne(b => b.Isbn, isbn =>
        {
            isbn.Property(i => i.Value)
                .HasColumnName("Isbn")
                .IsRequired()
                .HasMaxLength(13);
        });

        // Foreign Keys
        builder.Property(b => b.AuthorId)
            .IsRequired();

        builder.Property(b => b.CategoryId)
            .IsRequired();

        // Relationships
        builder.HasOne(b => b.Author)
            .WithMany()
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}