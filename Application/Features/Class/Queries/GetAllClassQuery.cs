using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ClassFeatures.Queries;
public class GetAllClassQuery : IRequest<ClasssBaseModel>
{

    public class GetAllClasssQueryHandler : IRequestHandler<GetAllClassQuery, ClasssBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllClasssQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClasssBaseModel> Handle(GetAllClassQuery query, CancellationToken cancellationToken)
        {
            var ClassList = await _context.classes
                .AsNoTracking()
                .Where(x=>!x.IsDeleted)
                .ToListAsync(cancellationToken: cancellationToken);

            if (ClassList == null)
            {
                return new ClasssBaseModel
                {
                    Data = null,
                    StatusCode = 200,
                    Messege = "No data found"
                };
            }
            return new ClasssBaseModel
            {
                Data = _mapper.Map<List<ClassDto>>(ClassList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }
    }
}
