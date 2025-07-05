namespace Catalog.CatalogBooks.Features.Categories.AddCategory;
internal class AddCategoryHandler
    (CatalogDbContext dbContext)
    : ICommandHandler<AddCategoryCommand, AddCategoryResult>
{
    public async Task<AddCategoryResult> Handle(
        AddCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var category = CreateCategory(request);
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddCategoryResult(category.Id);
    }

    private static Category CreateCategory(AddCategoryCommand request)
    {
        return Category.Create(
            Guid.NewGuid(),
            request.Name);
    }
}
