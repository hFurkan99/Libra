namespace Book.Books.Features.BookItems.InitializeBookItem;
public record InitializeBookItemCommand(
    Guid CatalogBookId,
    string? Barcode,
    string? Location,
    BookStatus? Status,
    string ConditionNote) 
    : ICommand<InitializeBookItemResult>;

public record InitializeBookItemResult(Guid BookItemId);

public class InitializeBookItemCommandValidator
    : AbstractValidator<InitializeBookItemCommand>
{
    public InitializeBookItemCommandValidator()
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