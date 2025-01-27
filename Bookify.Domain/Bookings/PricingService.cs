using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;
public class PricingService
{

    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        var currency = apartment.Price.Currency;
        var priceForPeriod = new Money(apartment.Price.Amount * period.lengthInDays, currency);
        decimal percenrageUpCharge = 0;
        foreach (var amenity in apartment.Amenities)
        {
            percenrageUpCharge += amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirCondition => 0.01m,
                Amenity.Parking => 0.01m,

                _ => 0
            };
        }

        var amenitiesUpCharge = Money.Zero(currency);

        if (percenrageUpCharge > 0)
        {
            amenitiesUpCharge = new Money(priceForPeriod.Amount * percenrageUpCharge, currency);
        }
        var totalPrice = Money.Zero();
        totalPrice += priceForPeriod;
        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }
        totalPrice += amenitiesUpCharge;
        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);

    }
}
