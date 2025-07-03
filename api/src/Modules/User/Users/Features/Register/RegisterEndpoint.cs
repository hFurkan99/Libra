namespace User.Users.Features.Register;
public record RegisterRequest(
    string Email,
    string Password,
    string UserName);

public record RegisterResponse(Guid UserId, string Email);

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/register", async (RegisterRequest request, ISender sender) =>
        {
            var command = request.Adapt<RegisterCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<RegisterResponse>();
            return Results.Created($"/users/{response.UserId}", response);
        })
        .WithName("RegisterUser")
        .Produces<RegisterResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Register User")
        .WithDescription("Register a new user in the system.");
    }
}