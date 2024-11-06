using Application.Features.ClassFeatures.Queries;
using Application.Features.CountryFeatures.Queries;
using Application.Features.StudentFeatures.Commands;
using Application.Features.StudentFeatures.Queries;
using Application.Model;
using MediatR;

namespace BlazorWebUI.Services;

public class StudentService 
{
    private readonly IMediator _mediator;

    public StudentService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var result = await _mediator.Send(new GetAllStudentQuery());
        return result?.Data ?? new List<StudentDto>();
    }
    public async Task<StudentDto> GetStudentsAsync(int id)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery { Id = id});
        return result?.Data ?? new StudentDto();
    }
    public async Task<StudentBaseModel> SaveStudentAsync(CreateStudentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<StudentBaseModel> UpdateStudentAsync(UpdateStudentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<StudentBaseModel> DeleteStudentAsync(int id)
    {
       return await _mediator.Send(new DeleteStudentByIdCommand { Id = id });
    }
}
