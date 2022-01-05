using Application.Features.CountryFeatures.Commands;
using Application.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper;
public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CreateCountryCommand>();
        CreateMap<CreateCountryCommand, Country>();
        CreateMap<UpdateCountryCommand, Country>();
        CreateMap<Country, CountryDto>();
    }
}
