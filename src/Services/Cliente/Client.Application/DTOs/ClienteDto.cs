namespace Client.Application.DTOs;

public record ClienteDto(
    string Nombre,
    Genero Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    string Password);