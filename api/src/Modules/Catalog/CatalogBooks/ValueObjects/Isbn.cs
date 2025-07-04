namespace Catalog.CatalogBooks.ValueObjects;
public record Isbn
{
    public string Value { get; }

    protected Isbn()
    { 
        Value = string.Empty;
    }

    private Isbn(string value)
    {
        Value = value;
    }

    public static Isbn Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ISBN cannot be empty.");

        if (value.Length != 10 && value.Length != 13)
            throw new ArgumentException("ISBN must be 10 or 13 characters.");
        return new Isbn(value);
    }
}