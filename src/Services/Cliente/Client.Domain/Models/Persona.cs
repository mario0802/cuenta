namespace Client.Domain.Models;
public abstract class Persona<TId> : Entity<TId>
{
    public string Nombre { get; protected set; } = default!;
    public Genero Genero { get; protected set; }
    public int Edad { get; protected set; }
    public string Identificacion { get; protected set; } = default!;
    public string Direccion { get; protected set; } = default!;
    public string Telefono { get; protected set; } = default!;

    protected Persona() { }

    protected Persona(
        string nombre,
        Genero genero,
        int edad,
        string identificacion,
        string direccion,
        string telefono)
    {
        Nombre = nombre;
        Genero = genero;
        Edad = edad;
        Identificacion = identificacion;
        Direccion = direccion;
        Telefono = telefono;
    }
}