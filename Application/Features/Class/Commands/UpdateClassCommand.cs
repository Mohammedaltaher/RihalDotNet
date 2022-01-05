using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ClassFeatures.Commands;
public class UpdateClassCommand : IRequest<ClassBaseModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ClassBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateClassCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClassBaseModel> Handle(UpdateClassCommand command, CancellationToken cancellationToken)
        {
            Class Class = _context.classes.Where(a => a.Id == command.Id).FirstOrDefault();

            if (Class == null)
            {
                return new ClassBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
               // Class = _mapper.Map<Class>(command);
                // _context.Classs.Update(Class);
               Class.Name = command.Name;
                await _context.SaveChangesAsync();
                return new ClassBaseModel
                {
                    Data = _mapper.Map<ClassDto>(Class),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
