using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Queries;
public class GetStudentPerClassReport : IRequest<StudentsPerClassBaseModel>
{

    public class GetAllStudentPerClassReportHandler : IRequestHandler<GetStudentPerClassReport, StudentsPerClassBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllStudentPerClassReportHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentsPerClassBaseModel> Handle(GetStudentPerClassReport query, CancellationToken cancellationToken)
        {
            var StudentList = await _context.students
                .Where(x => !x.IsDeleted)
                .GroupBy(c => c.Class.Name)
                .Select(m => new StudentsPerClassDto
                {
                    ClassName = m.Key,
                    NoStudents = m.Count(),
                })
                .ToListAsync(cancellationToken: cancellationToken);

            //ValueTask d = from m in StudentList
            // group m by m.ClassId into g
            // let TotalPoints = g.Count()
            //              orderby TotalPoints descending
            //              select new { User = g.Key, Username = g.Key.ClassId, TotalPoints };

            if (StudentList == null)
            {
                return new StudentsPerClassBaseModel
                {
                    Data = null,
                    StatusCode = 200,
                    Messege = "No data found"
                };
            }
            return new StudentsPerClassBaseModel
            {
                Data = StudentList,
                StatusCode = 200,
                Messege = "Data found"
            };
        }
    }
}
