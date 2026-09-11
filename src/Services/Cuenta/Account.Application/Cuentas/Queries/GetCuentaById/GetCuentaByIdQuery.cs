using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Cuentas.Queries.GetCuentaById;

public record GetCuentaByIdQuery(Guid Id)
    : IQuery<GetCuentaByIdResult>;

public record GetCuentaByIdResult(
    Guid Id,
    Guid ClienteId,
    string NumeroCuenta,
    TipoCuenta TipoCuenta,
    decimal SaldoInicial,
    EstadoCuenta Estado);

public class GetCuentaByIdQueryValidator : AbstractValidator<GetCuentaByIdQuery>
{
    public GetCuentaByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id es requerido");
    }
}