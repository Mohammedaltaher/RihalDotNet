using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using Application.Model;
using Application.Features.CountryFeatures.Queries;
using static Application.Features.CountryFeatures.Queries.GetAllCountryQuery;
using static Application.Features.CountryFeatures.Commands.CreateCountryCommand;
using static Application.Features.CountryFeatures.Queries.GetCountryByIdQuery;
using static Application.Features.CountryFeatures.Commands.UpdateCountryCommand;
using static Application.Features.CountryFeatures.Commands.DeleteCountryByIdCommand;

namespace WebApi.Test;
public class CountryTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public CountryTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.countries).Returns(Fixture.CreateContext().countries);
    }


    [Fact]
    public async Task Can_Get_All_CountrysAsync()
    {
        var handler = new GetAllCountrysQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        CountrysBaseModel result = await handler.Handle(new GetAllCountryQuery(), CancellationToken.None);
        var Country = result.Data;
        Assert.NotNull(Country);
        Assert.Equal(4, Country.Count);
       
    }


    [Fact]
    public async Task Can_Get_Country_By_IdAsync()
    {
        var handler = new GetCountryByIdQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        CountryBaseModel result = await handler.Handle(CountryData.MockGetCountryByIdQuery(), CancellationToken.None);
        var Country = result.Data;

        Assert.Equal("Sudan", Country.Name);
    }


    [Fact]
    public async Task Can_Add_CountryAsync()
    {
        var handler = new CreateCountryCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        CountryBaseModel result = await handler.Handle(CountryData.MockCreateCountryCommand(), CancellationToken.None);
        var Country = result.Data;

        Assert.Equal("Country2", Country.Name);
    }
   
    
    [Fact]
    public async Task Can_Update_CountryAsync()
    {
        var handler = new UpdateCountryCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        CountryBaseModel result = await handler.Handle(CountryData.MockUpdateCountryCommand(), CancellationToken.None);
        var Country = result.Data;

        Assert.Equal("Country25", Country.Name);
    }
  
    
    [Fact]
    public async Task Can_Delete_CountryAsync()
    {
        var handler = new DeleteCountryByIdCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        CountryBaseModel result = await handler.Handle(CountryData.MockDeleteCountryByIdCommand(), CancellationToken.None);
        var Country = result.Data;

        Assert.NotNull( Country.Name);
    }
}




