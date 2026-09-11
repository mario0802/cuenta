namespace Account.Domain.ValueObjects;

public record CuentaId
{
    public Guid Value { get; }
    private CuentaId(Guid value) => Value = value;
    public static CuentaId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CuentaId no puede ser nulo.");
        }
        return new CuentaId(value);
    }
    public static CuentaId New() => new(Guid.NewGuid());
}