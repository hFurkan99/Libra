namespace Catalog.CatalogBooks.Features.CatalogBooks.AddCatalogBook;
public record AddCatalogBookCommand(
    string Title,
    string Isbn,
    Guid AuthorId,
    Guid CategoryId)
    : ICommand<AddCatalogBookResult>;

public record AddCatalogBookResult(Guid CatalogBookId);

public class AddCatalogBookCommandValidator : AbstractValidator<AddCatalogBookCommand>
{
    public AddCatalogBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(200).WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(x => x.Isbn)
            .NotEmpty().WithMessage("ISBN cannot be empty.")
            .Must(isbn => isbn.Length == 10 || isbn.Length == 13)
            .WithMessage("ISBN must be 10 or 13 characters.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId cannot be empty.");
    }
}