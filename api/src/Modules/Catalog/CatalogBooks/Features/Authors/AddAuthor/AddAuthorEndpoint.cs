namespace Catalog.CatalogBooks.Features.Authors.AddAuthor;

public record AddAuthorRequest(string Name);
public record AddAuthorResponse(Guid AuthorId);

public class AddAuthorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/catalog/author", async (AddAuthorRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddAuthorCommand>();
            var result = await sender.Send(command);
            var response = new AddAuthorResponse(result.AuthorId);
            return Results.Created($"/catalog/author/{result.AuthorId}", response);
        })
        .WithName("AddAuthor")
        .Produces<AddAuthorResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog")
        .WithSummary("Create Author")
        .WithDescription("Create Authors");
    }
}