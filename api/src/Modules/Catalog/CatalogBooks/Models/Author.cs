namespace Catalog.CatalogBooks.Models;
public class Author : Entity<Guid>
{
    public string Name { get; private set; } = default!;
}
