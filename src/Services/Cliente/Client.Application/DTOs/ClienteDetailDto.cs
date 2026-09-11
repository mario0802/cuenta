namespace Client.Application.DTOs;

public record ClienteDetailDto(
    Guid Id,
    string Nombre,
    Genero Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    EstadoCliente Estado);