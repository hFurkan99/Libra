namespace Catalog.CatalogBooks.Features.Authors.AddAuthor;
internal class AddAuthorHandler
    (CatalogDbContext dbContext)
    : ICommandHandler<AddAuthorCommand, AddAuthorResult>
{
    public async Task<AddAuthorResult> Handle(
        AddAuthorCommand request, 
        CancellationToken cancellationToken)
    {
        var author = CreateAuthor(request);
        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddAuthorResult(author.Id);
    }

    private static Author CreateAuthor(AddAuthorCommand request)
    {
        return Author.Create(
            Guid.NewGuid(),
            request.Name);
    }
}
