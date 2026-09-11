namespace Account.Domain.ValueObjects;

public record MovimientoId
{
    public Guid Value { get; }
    private MovimientoId(Guid value) => Value = value;
    public static MovimientoId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("MovimientoId no puede ser nulo.");
        }
        return new MovimientoId(value);
    }
    public static MovimientoId New() => new(Guid.NewGuid());
}