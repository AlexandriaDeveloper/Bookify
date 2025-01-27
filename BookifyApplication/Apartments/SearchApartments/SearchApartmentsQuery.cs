using BookifyApplication.Abstractions.Messaging;

namespace BookifyApplication.Apartments.SearchApartments;
public sealed record SearchApartmentsQuery(DateOnly StratDate, DateOnly EndDate) : IQuery<IReadOnlyList<ApartmentResponse>>;
