namespace Client.Domain.ValueObjects;
public record ClienteId
{
    public Guid Value { get; }

    private ClienteId(Guid value) => Value = value;

    public static ClienteId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ClienteId no puede ser nulo.");
        }
        return new ClienteId(value);
    }

    public static ClienteId New() => new(Guid.NewGuid());
}