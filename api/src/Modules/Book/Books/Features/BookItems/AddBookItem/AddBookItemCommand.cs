namespace Book.Books.Features.BookItems.AddBookItem;
public record AddBookItemCommand(
    Guid CatalogBookId,
    string? Barcode,
    string? Location,
    BookStatus? Status,
    string ConditionNote) 
    : ICommand<AddBookItemResult>;

public record AddBookItemResult(Guid BookItemId);

public class AddBookItemCommandValidator 
    : AbstractValidator<AddBookItemCommand>
{
    public AddBookItemCommandValidator()
    {
        RuleFor(x => x.CatalogBookId)
            .NotEmpty()
            .WithMessage("Catalog Book ID cannot be empty.");

        RuleFor(x => x.Barcode)
            .MaximumLength(50)
            .WithMessage("Barcode cannot be longer than 50 characters.");

        RuleFor(x => x.Location)
            .MaximumLength(10)
            .WithMessage("Location cannot be longer than 10 characters.");

        RuleFor(x => x.ConditionNote)
            .MaximumLength(250)
            .WithMessage("Condition note cannot be longer than 250 characters.");
    }
}