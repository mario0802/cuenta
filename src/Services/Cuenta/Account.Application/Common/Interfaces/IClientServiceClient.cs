

using Account.Application.Common.Models;

namespace Account.Application.Common.Interfaces;

public interface IClientServiceClient
{
    Task<GetClienteByIdResponse> GetClienteByIdAsync(
        Guid clienteId,
        CancellationToken cancellationToken);
}
