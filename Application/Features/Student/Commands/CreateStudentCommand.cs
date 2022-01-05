using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Commands;
public class CreateStudentCommand : IRequest<StudentBaseModel>
{
    public string Name { get; set; }
    public int ClassId { get; set; }
    public int CountryId { get; set; }
    public DateTime? Birth_Date { get; set; }
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentBaseModel> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var Student = _mapper.Map<Student>(command);
            _context.students.Add(Student);

            await _context.SaveChangesAsync();
            return new StudentBaseModel
            {
                Data = _mapper.Map<StudentDto>(Student),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }
}

