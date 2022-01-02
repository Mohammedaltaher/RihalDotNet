using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Queries;
public class GetAllStudentQuery : IRequest<StudentsBaseModel>
{

    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentQuery, StudentsBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllStudentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentsBaseModel> Handle(GetAllStudentQuery query, CancellationToken cancellationToken)
        {
            var StudentList = await _context.students
                .AsNoTracking()
                //.Include(x=>x.CreatedBy)
                .ToListAsync(cancellationToken: cancellationToken);

            if (StudentList == null)
            {
                return new StudentsBaseModel
                {
                    Data = null,
                    StatusCode = 200,
                    Messege = "No data found"
                };
            }
            return new StudentsBaseModel
            {
                Data = _mapper.Map<List<StudentDto>>(StudentList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }
    }
}
