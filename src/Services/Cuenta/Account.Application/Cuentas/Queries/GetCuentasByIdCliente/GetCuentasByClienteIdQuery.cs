namespace Account.Application.Cuentas.Queries.GetCuentasByIdCliente;

public record GetCuentasByClienteIdQuery(Guid ClienteId)
    : IQuery<GetCuentasByClienteIdResult>;

public record GetCuentasByClienteIdResult(List<CuentaResumenDto> Cuentas);

public record CuentaResumenDto(
    Guid Id,
    string NumeroCuenta,
    TipoCuenta TipoCuenta,
    decimal SaldoInicial,
    EstadoCuenta Estado);

public class GetCuentasByClienteIdQueryValidator : AbstractValidator<GetCuentasByClienteIdQuery>
{
    public GetCuentasByClienteIdQueryValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("ClienteId es requerido");
    }
}