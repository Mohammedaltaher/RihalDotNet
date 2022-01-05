using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Queries;
public class GetStudentAverageAgeReport : IRequest<double>
{

    public class GetAllStudentPerClassReportHandler : IRequestHandler<GetStudentAverageAgeReport, double>
    {
        private readonly IApplicationDbContext _context;
        public GetAllStudentPerClassReportHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<double> Handle(GetStudentAverageAgeReport query, CancellationToken cancellationToken)
        {
            var StudentList = await _context.students
                .Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken: cancellationToken);
            if (StudentList == null)
            {
                return 0;
            }
            return Math.Truncate(StudentList.Average(dt => (DateTime.Now - dt.Birth_Date).TotalDays) / 365) ;
        }
    }
}