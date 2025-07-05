namespace Catalog.CatalogBooks.Models;
public class Category : Entity<Guid>
{
    public string Name { get; private set; } = default!;

    protected Category() { }

    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        return new Category(id, name);
    }
}
