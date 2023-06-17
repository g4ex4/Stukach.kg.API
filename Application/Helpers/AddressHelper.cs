using System.Text;
using Domain.Models;

namespace Application.Helpers;

public static class AddressHelper
{
    public static string GetFullAddress(Region? region, District? district, City? city)
    {
        var result = new StringBuilder();
        if (region is not null) result.Append(region.Name);
        if (district is not null) result.Append(district.Name);
        if (city is not null) result.Append(city.Name);
        return result.ToString();
    }
}