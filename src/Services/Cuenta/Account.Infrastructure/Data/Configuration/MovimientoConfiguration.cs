using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Data.Configuration;

public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.ToTable("Movimientos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                movimientoId => movimientoId.Value,
                dbId => MovimientoId.Of(dbId))
            .ValueGeneratedNever();

        builder.Property(m => m.CuentaId)
            .HasConversion(
                cuentaId => cuentaId.Value,
                dbId => CuentaId.Of(dbId))
            .IsRequired();

        builder.HasIndex(m => m.CuentaId);

        builder.Property(m => m.TipoMovimiento)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.Fecha)
            .IsRequired();

        builder.Property(m => m.Valor)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(m => m.Saldo)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}