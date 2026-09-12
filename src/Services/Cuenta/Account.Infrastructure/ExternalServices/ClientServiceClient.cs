using Account.Application.Common.Interfaces;
using Account.Application.Common.Models;
using BuildingBlocks.Exceptions;
using System.Net.Http.Json;

namespace Account.Infrastructure.ExternalServices
{
    public class ClientServiceClient(HttpClient httpClient) : IClientServiceClient
    {
        public async Task<GetClienteByIdResponse> GetClienteByIdAsync(Guid clienteId, CancellationToken cancellationToken)
        {
            var url = $"/clientes/{clienteId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new BadRequestException($"Client Service devolvio: {(int)response.StatusCode}");
            var cliente = await response.Content.ReadFromJsonAsync<ClienteDto>(cancellationToken: cancellationToken);
            return new GetClienteByIdResponse(cliente ?? ClienteDto.Empty);
        }
    }
}
