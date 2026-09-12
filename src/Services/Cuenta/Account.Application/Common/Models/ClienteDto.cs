namespace Account.Application.Common.Models;

public record ClienteDto(
    Guid Id,
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    string Estado
    )
{
    public static ClienteDto Empty => new(
        Guid.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty);
}

public record GetClienteByIdResponse(ClienteDto Cliente);