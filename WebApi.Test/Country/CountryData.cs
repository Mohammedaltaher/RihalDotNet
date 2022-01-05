using System.Collections.Generic;
using Application.Features.CountryFeatures.Commands;
using Application.Features.CountryFeatures.Queries;
using Domain.Entities;

namespace WebApi.Test;
public static class CountryData
{
    public static List<Country> MockCountrySamples()
    {
        var Country = new List<Country>()
            {
                new Country()
                {
                    Name = "Country2",
                },
                new Country()
                {
                    Name = "Country",
                }
            };

        return Country;
    }
    public static GetCountryByIdQuery MockGetCountryByIdQuery()
    {
        return new GetCountryByIdQuery()
        {
            Id = 1
        };
    }
    public static CreateCountryCommand MockCreateCountryCommand()
    {
        return new CreateCountryCommand()
        {
            Name = "Country2",
        };
    }
    public static UpdateCountryCommand MockUpdateCountryCommand()
    {
        return new UpdateCountryCommand()
        {
            Id = 1,
            Name = "Country25",
        };
    }
    public static DeleteCountryByIdCommand MockDeleteCountryByIdCommand()
    {
        return new DeleteCountryByIdCommand()
        {
            Id = 2
        };
    }

}
