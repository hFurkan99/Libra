
namespace Book.Books.Features.BookItems.AddBookItem;
public record AddBookItemRequest(
    Guid CatalogBookId,
    string? Barcode,
    string? Location,
    BookStatus? Status,
    string ConditionNote) 
    : ICommand<AddBookItemResponse>;

public record AddBookItemResponse(Guid BookItemId);

public class AddBookItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/books/item", 
            async (AddBookItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddBookItemCommand>();
            var result = await sender.Send(command);
            var response = new AddBookItemResponse(result.BookItemId);
            return Results.Created($"/books/item/{result.BookItemId}", response);
        })
        .WithName("AddBookItem")
        .Produces<AddBookItemResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Books")
        .WithSummary("Create Book Item")
        .WithDescription("Create Book Items");
    }
}
