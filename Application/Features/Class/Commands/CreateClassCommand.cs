using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ClassFeatures.Commands;
public class CreateClassCommand : IRequest<ClassBaseModel>
{
    public string Name { get; set; }
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ClassBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateClassCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClassBaseModel> Handle(CreateClassCommand command, CancellationToken cancellationToken)
        {
            var Class = _mapper.Map<Class>(command);
            _context.classes.Add(Class);

            await _context.SaveChangesAsync();
            return new ClassBaseModel
            {
                Data = _mapper.Map<ClassDto>(Class),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }
}

