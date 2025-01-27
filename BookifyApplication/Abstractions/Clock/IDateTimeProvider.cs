namespace BookifyApplication.Abstractions.Clock;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
