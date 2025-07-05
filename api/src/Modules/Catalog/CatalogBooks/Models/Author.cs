namespace Catalog.CatalogBooks.Models;
public class Author : Entity<Guid>
{
    public string Name { get; private set; } = default!;

    protected Author() {}

    private Author(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Author Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        return new Author(id, name);
    }
}
