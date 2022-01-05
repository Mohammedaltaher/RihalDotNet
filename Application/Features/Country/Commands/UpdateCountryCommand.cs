using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Commands;
public class UpdateCountryCommand : IRequest<CountryBaseModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountryBaseModel> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
        {
            Country Country = _context.countries.Where(a => a.Id == command.Id).FirstOrDefault();

            if (Country == null)
            {
                return new CountryBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
               // Country = _mapper.Map<Country>(command);
                // _context.Countrys.Update(Country);
               Country.Name = command.Name;
                await _context.SaveChangesAsync();
                return new CountryBaseModel
                {
                    Data = _mapper.Map<CountryDto>(Country),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
