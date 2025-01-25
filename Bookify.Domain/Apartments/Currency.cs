namespace Bookify.Domain.Apartments;

public record Currency
{
    internal static Currency None = new Currency("");
    public static Currency USD = new Currency("USD");
    public static Currency EUR = new Currency("EUR");
  

    private Currency(string code)=> Code =code;
    public string Code { get;init ; }
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code)?? throw new ApplicationException("The Currency Code Invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = new List<Currency>() {
    USD,
    EUR,
    // Add more currencies here if needed
    };
}