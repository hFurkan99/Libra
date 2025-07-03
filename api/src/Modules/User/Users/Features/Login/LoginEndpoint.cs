namespace User.Users.Features.Login;
public record LoginRequest(
    string Email,
    string Password);

public record LoginResponse(string Token);
public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/login", async (LoginRequest request, ISender sender) =>
        {
            var command = request.Adapt<LoginCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<LoginResponse>();
            return Results.Ok(response);
        })
        .WithName("LoginUser")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Login User")
        .WithDescription("Authenticate a user and return a JWT token.");
    }
}
