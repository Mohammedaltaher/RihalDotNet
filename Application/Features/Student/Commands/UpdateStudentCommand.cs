using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Commands;
public class UpdateStudentCommand : IRequest<StudentBaseModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentBaseModel> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            Student Student = _context.students.Where(a => a.Id == command.Id).FirstOrDefault();

            if (Student == null)
            {
                return new StudentBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
                Student = _mapper.Map<Student>(command);
                //    _context.Students.Update(Student);
                await _context.SaveChangesAsync();
                return new StudentBaseModel
                {
                    Data = _mapper.Map<StudentDto>(Student),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
