namespace Catalog.CatalogBooks.Models;
public class Category : Entity<Guid>
{
    public string Name { get; private set; } = default!;
}
