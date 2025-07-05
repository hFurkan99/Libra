namespace Catalog.CatalogBooks.Features.Categories.AddCategory;

public record AddCategoryRequest(string Name);
public record AddCategoryResponse(Guid CategoryId);

public class AddCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/catalog/category", 
            async (AddCategoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddCategoryCommand>();
            var result = await sender.Send(command);
            var response = new AddCategoryResponse(result.CategoryId);
            return Results.Created($"/catalog/category/{result.CategoryId}", response);
        })
        .WithName("AddCategory")
        .Produces<AddCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog")
        .WithSummary("Create Category")
        .WithDescription("Create Categorys");
    }
}