using Application.Features.ClassFeatures.Queries;
using Application.Features.CountryFeatures.Queries;
using Application.Features.CountryFeatures.Commands;
using Application.Model;
using MediatR;

namespace BlazorWebUI.Services;

public class CountryService 
{
    private readonly IMediator _mediator;

    public CountryService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<CountryDto> GetCountryAsync(int id)
    {
        var result = await _mediator.Send(new GetCountryByIdQuery { Id = id });
        return result?.Data ?? new CountryDto();
    }
    public async Task<List<CountryDto>> GetAllCountrysAsync()
    {
        var result = await _mediator.Send(new GetAllCountryQuery());
        return result?.Data ?? new List<CountryDto>();
    }
    public async Task<CountryBaseModel> SaveCountryAsync(CreateCountryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CountryBaseModel> UpdateCountryAsync(UpdateCountryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CountryBaseModel> DeleteCountryAsync(int id)
    {
       return await _mediator.Send(new DeleteCountryByIdCommand { Id = id });
    }
}
