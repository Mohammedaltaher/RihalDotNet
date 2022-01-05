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
public class GetStudentPerCountryReport : IRequest<StudentsPerCountryBaseModel>
{

    public class GetAllStudentPerCountryReportHandler : IRequestHandler<GetStudentPerCountryReport, StudentsPerCountryBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllStudentPerCountryReportHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentsPerCountryBaseModel> Handle(GetStudentPerCountryReport query, CancellationToken cancellationToken)
        {
            var StudentList = await _context.students
                .Where(x => !x.IsDeleted)
                .GroupBy(c => c.Country.Name)
                .Select(m => new StudentsPerCountryDto
                {
                    CountryName = m.Key,
                    NoStudents = m.Count(),
                })
                .ToListAsync(cancellationToken: cancellationToken);

            //ValueTask d = from m in StudentList
            // group m by m.CountryId into g
            // let TotalPoints = g.Count()
            //              orderby TotalPoints descending
            //              select new { User = g.Key, Username = g.Key.CountryId, TotalPoints };

            if (StudentList == null)
            {
                return new StudentsPerCountryBaseModel
                {
                    Data = null,
                    StatusCode = 200,
                    Messege = "No data found"
                };
            }
            return new StudentsPerCountryBaseModel
            {
                Data = StudentList,
                StatusCode = 200,
                Messege = "Data found"
            };
        }
    }
}
