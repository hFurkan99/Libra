namespace Book.Data;
public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
{
    public DbSet<BookItem> BookItems { get; set; } = null!;
    public DbSet<BookStock> BookStocks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("book");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
