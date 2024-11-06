using Application.Features.ClassFeatures.Queries;
using Application.Features.ClassFeatures.Commands;
using Application.Model;
using MediatR;

namespace BlazorWebUI.Services;

public class ClassService 
{
    private readonly IMediator _mediator;

    public ClassService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ClassDto> GetClasssAsync(int id)
    {
        var result = await _mediator.Send(new GetClassByIdQuery { Id = id });
        return result?.Data ?? new ClassDto();
    }
    public async Task<List<ClassDto>> GetAllClasssAsync()
    {
        var result = await _mediator.Send(new GetAllClassQuery());
        return result?.Data ?? new List<ClassDto>();
    }
    public async Task<ClassBaseModel> SaveClassAsync(CreateClassCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ClassBaseModel> UpdateClassAsync(UpdateClassCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ClassBaseModel> DeleteClassAsync(int id)
    {
       return await _mediator.Send(new DeleteClassByIdCommand { Id = id });
    }
}
