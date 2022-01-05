using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ClassFeatures.Queries;
public class GetClassByIdQuery : IRequest<ClassBaseModel>
{
    public int Id { get; set; }
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetClassByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<ClassBaseModel> Handle(GetClassByIdQuery query, CancellationToken cancellationToken)
        {
            var Class = _context.classes.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Class == null)
            {
                return Task.FromResult(new ClassBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                });
            }
            return Task.FromResult(new ClassBaseModel
            {
                Data = _mapper.Map<ClassDto>(Class),
                Classdb = Class,
                StatusCode = 200,
                Messege = "Data found"
            });
        }
    }
}
