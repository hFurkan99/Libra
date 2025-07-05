namespace Catalog.CatalogBooks.Features.Categories.AddCategory;
public record AddCategoryCommand(string Name) 
    : ICommand<AddCategoryResult>;

public record AddCategoryResult(Guid CategoryId);

public class AddCategoryCommandValidator 
    : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Name cannot be longer than 100 characters.");
    }
}