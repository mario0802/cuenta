using BuildingBlocks.Exceptions;
using BuildingBlocks.Pagination;
using Client.Application.Common.Interfaces;
using Client.Application.Common.Models.AccountService;
using System.Net.Http.Json;

namespace Client.Infrastructure.ExternalServices;

public class AccountServiceClient(HttpClient httpClient) : IAccountServiceClient
{
    public async Task<GetMovimientosByCuentaResponse> GetMovimientosByCuentaAsync(
        Guid cuentaId,
        DateTime? fechaDesde,
        DateTime? fechaHasta,
        CancellationToken cancellationToken)
    {
        var query = new List<string>();
        if (fechaDesde.HasValue)
            query.Add($"fechaDesde={fechaDesde.Value:O}");

        if (fechaHasta.HasValue)
            query.Add($"fechaHasta={fechaHasta.Value:O}");

        var url = $"/movimientos/cuentas/{cuentaId}/reporte?{string.Join("&", query)}";

        var response = await httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new BadRequestException(
                $"Account service returned {(int)response.StatusCode} for cuenta {cuentaId}");
        }

        var result = await response.Content
            .ReadFromJsonAsync<GetMovimientosByCuentaResponse>(cancellationToken: cancellationToken);

        return result ?? new GetMovimientosByCuentaResponse([]);
    }
}