namespace User.Users.Features.Register;
public record RegisterCommand(
    string Email, 
    string Password, 
    string UserName) 
    : ICommand<RegisterResult>;

public record RegisterResult(Guid UserId, string Email);

internal class RegisterHandler(
    UserManager<AppUser> userManager)
    : ICommandHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand command, 
        CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = command.UserName,
            Email = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if(!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User registration failed: {errors}");
        }

        return new RegisterResult(user.Id, user.Email);
    }
}
