namespace Bookify.Domain.Abstractions;
public record Error(string code, string Name)
{
    public static Error None = new Error(string.Empty, string.Empty);
    public static Error NullValue = new Error("Error.NullValue", "Null value was Provided");
}
