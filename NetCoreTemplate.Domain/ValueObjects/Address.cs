using System.Text.RegularExpressions;
using NetCoreTemplate.Domain.Exceptions;

namespace NetCoreTemplate.Domain.ValueObjects;

public record Address
{
    public string Street { get; }
    public string City { get; }
    public string PostalCode { get; }
    public string HouseNumber { get; }
    
    private Address() {}

    private Address(string street, string postalCode, string city, string houseNumber)
    {
        Street = street;
        PostalCode = postalCode;
        City = city;
        HouseNumber = houseNumber;
    }

    public static Address Create(string street, string postalCode, string city, string plotNumber = null,
        string houseNumber = null)
    {
        if (postalCode != null)
        {
            Regex postalCodeValidation = new Regex(@"^\d{2}-\d{3}$");
            var isMatched = postalCodeValidation.IsMatch(postalCode);
            if (!isMatched)
                throw new InvalidPostalCodeException();
        }

        if (street != null)
            if (string.IsNullOrWhiteSpace(street)) throw new EmptyOrWhiteSpaceException("Provide valid street name.");
        
        if (city != null)
            if (string.IsNullOrWhiteSpace(city)) throw new EmptyOrWhiteSpaceException("Provide valid city name");

        if (plotNumber != null)
            if (string.IsNullOrWhiteSpace(plotNumber)) throw new EmptyOrWhiteSpaceException("Provide valid plot number");

        if (houseNumber != null)
            if (string.IsNullOrWhiteSpace(houseNumber)) throw new EmptyOrWhiteSpaceException("Provide valid house number");
        

        return new Address(street, postalCode, city, houseNumber);
    }
}