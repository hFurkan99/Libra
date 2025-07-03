namespace User.Users.Features.Login;
public record class LoginCommand(
    string Email, 
    string Password) 
    : ICommand<LoginResult>;

public record class LoginResult(string Token);

internal class LoginHandler(
    UserManager<AppUser> userManager, 
    SignInManager<AppUser> signInManager,
    IConfiguration configuration)
    : ICommandHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand command, 
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email) 
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        var result = await signInManager
            .CheckPasswordSignInAsync(user, command.Password, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = GenerateJwtToken(user);

        return new LoginResult(token);
    }

    private string GenerateJwtToken(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenExpirationInMinutes = double.Parse(configuration["Jwt:TokenExpirationInMinutes"]!);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(tokenExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
