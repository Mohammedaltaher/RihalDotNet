using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Queries;
public class GetCountryByIdQuery : IRequest<CountryBaseModel>
{
    public int Id { get; set; }
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCountryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<CountryBaseModel> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var Country = _context.countries.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Country == null)
            {
                return Task.FromResult(new CountryBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                });
            }
            return Task.FromResult(new CountryBaseModel
            {
                Data = _mapper.Map<CountryDto>(Country),
                Countrydb = Country,
                StatusCode = 200,
                Messege = "Data found"
            });
        }
    }
}
