using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Queries;
public class GetStudentByIdQuery : IRequest<StudentBaseModel>
{
    public int Id { get; set; }
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<StudentBaseModel> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
        {
            var Student = _context.students.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Student == null)
            {
                return Task.FromResult(new StudentBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                });
            }
            return Task.FromResult(new StudentBaseModel
            {
                Data = _mapper.Map<StudentDto>(Student),
                //Studentdb = Student,
                StatusCode = 200,
                Messege = "Data found"
            });
        }
    }
}
