
namespace Client.Domain.Models;

public class Cliente : Persona<ClienteId>
{
    public string Password { get; private set; } = default!;
    public EstadoCliente Estado { get; private set; }

    private Cliente() { } 

    private Cliente(
        ClienteId id,
        string nombre,
        Genero genero,
        int edad,
        string identificacion,
        string direccion,
        string telefono,
        string password,
        EstadoCliente estado)
        : base(nombre, genero, edad, identificacion, direccion, telefono)
    {
        Id = id;
        Password = password;
        Estado = estado;
    }

    public static Cliente Crear(
        string nombre,
        Genero genero,
        int edad,
        string identificacion,
        string direccion,
        string telefono,
        string password)
    {
        return new Cliente(
            ClienteId.New(),
            nombre,
            genero,
            edad,
            identificacion,
            direccion,
            telefono,
            password,
            EstadoCliente.Activo);
    }

    public void ActualizarDatos(
    string nombre,
    Genero genero,
    int edad,
    string direccion,
    string telefono)
    {
        Nombre = nombre;
        Genero = genero;
        Edad = edad;
        Direccion = direccion;
        Telefono = telefono;
    }

    public void CambiarContrasena(string password) => Password = password;
    public void Activar() => Estado = EstadoCliente.Activo;
    public void Inactivar() => Estado = EstadoCliente.Inactivo;
}
