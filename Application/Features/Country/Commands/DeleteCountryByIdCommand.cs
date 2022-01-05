using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Commands;
public class DeleteCountryByIdCommand : IRequest<CountryBaseModel>
{
    public int Id { get; set; }
    public class DeleteCountryByIdCommandHandler : IRequestHandler<DeleteCountryByIdCommand, CountryBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteCountryByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountryBaseModel> Handle(DeleteCountryByIdCommand command, CancellationToken cancellationToken)
        {
            var Country =  _context.countries.Where(a => a.Id == command.Id).FirstOrDefault();
            if (Country == null)
            {
                return new CountryBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            };
            Country.IsDeleted = true;
            //    _context.Countrys.Update(Country);
            await _context.SaveChangesAsync();
            return new CountryBaseModel
            {
                Data = _mapper.Map<CountryDto>(Country),
                StatusCode = 200,
                Messege = "Data has been Deleted"
            };
        }
    }
}
