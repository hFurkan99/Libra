namespace Catalog.CatalogBooks.Models;
public class CatalogBook : Aggregate<Guid>
{
    public string Title { get; private set; } = default!;
    public Isbn Isbn { get; private set; } = default!;
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; private set; }

    public Author? Author { get; private set; }
    public Category? Category { get; private set; }

    protected CatalogBook()
    {
    }

    private CatalogBook(Guid id, string title, Isbn isbn, Guid authorId, Guid categoryId)
    {
        Id = id;
        Title = title;
        Isbn = isbn;
        AuthorId = authorId;
        CategoryId = categoryId;
    }

    public static CatalogBook Create(Guid id, string title, Isbn isbn, Guid authorId, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        
        if (isbn is null)
            throw new ArgumentNullException(nameof(isbn), "ISBN cannot be null.");

        return new CatalogBook(id, title, isbn, authorId, categoryId);
    }
}