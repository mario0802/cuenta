using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Client.IntegrationTests.Controllers;

public class ClienteControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ClienteControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostCliente_ConDatosValidos_DeberiaCrearClienteYRetornar201()
    {
        // Arrange
        var request = new
        {
            Nombre = "Juan Perez",
            Genero = "Masculino",
            Edad = 30,
            Identificacion = "1234567890",
            Direccion = "Av. Siempre Viva 123",
            Telefono = "0999999999",
            Password = "Password123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/clientes", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await response.Content.ReadFromJsonAsync<ClienteResponse>();
        body!.Id.Should().NotBeEmpty();
        body.Nombre.Should().Be("Juan Perez");
    }

    [Fact]
    public async Task GetCliente_ConIdInexistente_DeberiaRetornar404()
    {
        // Act
        var response = await _client.GetAsync($"/clientes/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private record ClienteResponse(Guid Id, string Nombre);
}
