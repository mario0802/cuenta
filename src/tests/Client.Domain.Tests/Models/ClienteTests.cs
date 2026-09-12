using Client.Domain.Enums;
using Client.Domain.Models;
using FluentAssertions;

namespace Client.Domain.Tests.Models;

public class ClienteTests
{
    private static Cliente CrearClienteValido(
        string nombre = "Juan Perez",
        Genero genero = Genero.Masculino,
        int edad = 30,
        string identificacion = "1234567890",
        string direccion = "Av. Siempre Viva 123",
        string telefono = "0999999999",
        string password = "Password123!")
    {
        return Cliente.Crear(nombre, genero, edad, identificacion, direccion, telefono, password);
    }

    // ---------- Crear ----------
    [Fact]
    public void Crear_ConDatosValidos_DeberiaCrearClienteConEstadoActivo()
    {
        // Act
        var cliente = CrearClienteValido();

        // Assert
        cliente.Should().NotBeNull();
        cliente.Id.Should().NotBeNull();
        cliente.Nombre.Should().Be("Juan Perez");
        cliente.Genero.Should().Be(Genero.Masculino);
        cliente.Edad.Should().Be(30);
        cliente.Identificacion.Should().Be("1234567890");
        cliente.Direccion.Should().Be("Av. Siempre Viva 123");
        cliente.Telefono.Should().Be("0999999999");
        cliente.Password.Should().Be("Password123!");
        cliente.Estado.Should().Be(EstadoCliente.Activo);
    }

    [Fact]
    public void Crear_DosClientes_DeberianTenerIdsDiferentes()
    {
        var cliente1 = CrearClienteValido();
        var cliente2 = CrearClienteValido();

        cliente1.Id.Should().NotBe(cliente2.Id);
    }

    [Fact]
    public void ActualizarDatos_DeberiaActualizarSoloLosCamposPermitidos()
    {
        // Arrange
        var cliente = CrearClienteValido();
        var identificacionOriginal = cliente.Identificacion;
        var passwordOriginal = cliente.Password;

        // Act
        cliente.ActualizarDatos(
            nombre: "Maria Lopez",
            genero: Genero.Femenino,
            edad: 25,
            direccion: "Nueva Direccion 456",
            telefono: "0988888888");

        // Assert
        cliente.Nombre.Should().Be("Maria Lopez");
        cliente.Genero.Should().Be(Genero.Femenino);
        cliente.Edad.Should().Be(25);
        cliente.Direccion.Should().Be("Nueva Direccion 456");
        cliente.Telefono.Should().Be("0988888888");

        // No deben cambiar
        cliente.Identificacion.Should().Be(identificacionOriginal);
        cliente.Password.Should().Be(passwordOriginal);
    }
}