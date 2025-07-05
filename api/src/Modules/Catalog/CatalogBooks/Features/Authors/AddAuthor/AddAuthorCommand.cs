namespace Catalog.CatalogBooks.Features.Authors.AddAuthor;
public record AddAuthorCommand(string Name) 
    : ICommand<AddAuthorResult>;

public record AddAuthorResult(Guid AuthorId);

public class AddAuthorCommandValidator 
    : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Name cannot be longer than 100 characters.");
    }
}