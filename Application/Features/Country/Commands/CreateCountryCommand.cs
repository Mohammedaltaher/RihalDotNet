using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Commands;
public class CreateCountryCommand : IRequest<CountryBaseModel>
{
    public string Name { get; set; }
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateCountryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountryBaseModel> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            var Country = _mapper.Map<Country>(command);
            _context.countries.Add(Country);

            await _context.SaveChangesAsync();
            return new CountryBaseModel
            {
                Data = _mapper.Map<CountryDto>(Country),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }
}

