using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Client.Infrastructure.Data.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                clienteId => clienteId.Value,
                dbId => ClienteId.Of(dbId))
            .ValueGeneratedNever();

        builder.Property(c => c.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Genero)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.Edad)
            .IsRequired();

        builder.Property(c => c.Identificacion)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(c => c.Identificacion)
            .IsUnique();

        builder.Property(c => c.Direccion)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(c => c.Telefono)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.Password)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
    }
}
