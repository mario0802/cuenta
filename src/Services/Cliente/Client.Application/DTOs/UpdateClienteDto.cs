
namespace Client.Application.DTOs;

public record UpdateClienteDto(
    Guid Id,
    string Nombre,
    Genero Genero,
    int Edad,
    string Direccion,
    string Telefono);