using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ClassFeatures.Commands;
public class DeleteClassByIdCommand : IRequest<ClassBaseModel>
{
    public int Id { get; set; }
    public class DeleteClassByIdCommandHandler : IRequestHandler<DeleteClassByIdCommand, ClassBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteClassByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClassBaseModel> Handle(DeleteClassByIdCommand command, CancellationToken cancellationToken)
        {
            var Class =  _context.classes.Where(a => a.Id == command.Id).FirstOrDefault();
            if (Class == null)
            {
                return new ClassBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            };
            Class.IsDeleted = true;
            //    _context.Classs.Update(Class);
            await _context.SaveChangesAsync();
            return new ClassBaseModel
            {
                Data = _mapper.Map<ClassDto>(Class),
                StatusCode = 200,
                Messege = "Data has been Deleted"
            };
        }
    }
}
