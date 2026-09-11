using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Data.Configuration;

public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
{
    public void Configure(EntityTypeBuilder<Cuenta> builder)
    {
        builder.ToTable("Cuentas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                cuentaId => cuentaId.Value,
                dbId => CuentaId.Of(dbId))
            .ValueGeneratedNever();

        builder.Property(c => c.ClienteId)
            .HasConversion(
                clienteId => clienteId.Value,
                dbId => ClienteId.Of(dbId))
            .IsRequired();

        builder.HasIndex(c => c.ClienteId);

        builder.Property(c => c.NumeroCuenta)
            .HasMaxLength(12)
            .IsRequired();

        builder.HasIndex(c => c.NumeroCuenta)
            .IsUnique();

        builder.Property(c => c.TipoCuenta)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.SaldoInicial)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(c => c.Movimientos)
            .WithOne()
            .HasForeignKey(m => m.CuentaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Movimientos)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}