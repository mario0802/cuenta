using Client.Application.Common.Models.AccountService;

namespace Client.Application.Common.Interfaces;

public interface IAccountServiceClient
{
    Task<GetMovimientosByCuentaResponse> GetMovimientosByCuentaAsync(
        Guid cuentaId,
        DateTime? fechaDesde,
        DateTime? fechaHasta,
        CancellationToken cancellationToken);
}