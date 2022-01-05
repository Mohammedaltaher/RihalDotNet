using Application.Interfaces;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Queries;
public class GetAllCountryQuery : IRequest<CountrysBaseModel>
{

    public class GetAllCountrysQueryHandler : IRequestHandler<GetAllCountryQuery, CountrysBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCountrysQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountrysBaseModel> Handle(GetAllCountryQuery query, CancellationToken cancellationToken)
        {
            var CountryList = await _context.countries
                .AsNoTracking()
                .Where(x=>!x.IsDeleted)
                .ToListAsync(cancellationToken: cancellationToken);

            if (CountryList == null)
            {
                return new CountrysBaseModel
                {
                    Data = null,
                    StatusCode = 200,
                    Messege = "No data found"
                };
            }
            return new CountrysBaseModel
            {
                Data = _mapper.Map<List<CountryDto>>(CountryList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }
    }
}
