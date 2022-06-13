namespace Catalog.Domain.Exceptions;

public sealed class CityNotFoundException : NotFoundException
{
    public CityNotFoundException(int cityId)
        : base($"The city with the identifier {cityId} was not found.")
    {
    }

    public CityNotFoundException(string cityName)
        : base($"The city with the Name {cityName} was not found.")
    {
    }
}