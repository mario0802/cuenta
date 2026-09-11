namespace BuildingBlocks.Exceptions;

public class DomainException: Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}