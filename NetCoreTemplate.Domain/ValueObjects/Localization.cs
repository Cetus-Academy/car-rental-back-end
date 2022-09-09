using System.Text.RegularExpressions;
using NetCoreTemplate.Domain.Exceptions;

namespace NetCoreTemplate.Domain.ValueObjects;

public record Localization
{
    public double Latitude { get; }
    public double Longitude { get; }

    private Localization(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Localization Create(double latitude, double longitude)
    {
        Regex propLocalization = new Regex(@"^(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)$");
        
        bool isMatch = propLocalization.IsMatch($"{latitude.ToString().Replace(',','.')}, {longitude.ToString().Replace(',','.')}");

        if (!isMatch)
            throw new InvalidCoordinatesException();
        
        return new Localization(latitude, longitude);
    }
}