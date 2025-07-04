namespace Catalog.CatalogBooks.Features.CatalogBooks.AddCatalogBook;
public record AddCatalogBookRequest(
    Guid Id,
    string Title,
    string Isbn,
    Guid AuthorId,
    Guid CategoryId);

public record AddCatalogBookResponse(Guid CatalogBookId);

public class AddCatalogBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/catalog/books", async (AddCatalogBookRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddCatalogBookCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<AddCatalogBookResponse>();
            return Results.Created($"/catalog/books/{result.CatalogBookId}", response);
        })
        .WithName("AddCatalogBook")
        .Produces<AddCatalogBookResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("CatalogBooks")
        .WithSummary("Create Catalog Book")
        .WithDescription("Create Catalog Books");
    }
}
