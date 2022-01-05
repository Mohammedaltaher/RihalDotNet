using Application.Common.Mapper;
using AutoMapper;

namespace WebApi.Test;
public static class MockServices
{

    public static IMapper GetMockedMapper<T>()
    {
        var mappingConfig = new MapperConfiguration(profile =>
        {
            profile.AddProfile(new StudentProfile());
            profile.AddProfile(new CountryProfile());
            profile.AddProfile(new ClassProfile());
        });
        var moq = mappingConfig.CreateMapper();
        return moq;
    }

   
}
