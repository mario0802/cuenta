namespace Client.Application.Clientes.Queries.Login;

public record LoginQuery(LoginDto Login)
    : IQuery<LoginResult>;

public record LoginDto(
    string Identificacion,
    string Password);

public record LoginResult(
    string AccessToken,
    TimeSpan ExpiresIn);

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Login.Identificacion).NotEmpty().WithMessage("Identificacion is required");
        RuleFor(x => x.Login.Password).NotEmpty().WithMessage("Password is required");
    }
}