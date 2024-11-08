﻿using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StudentFeatures.Commands;
public class DeleteStudentByIdCommand : IRequest<StudentBaseModel>
{
    public int Id { get; set; }
    public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommand, StudentBaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteStudentByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentBaseModel> Handle(DeleteStudentByIdCommand command, CancellationToken cancellationToken)
        {
            var Student =  _context.students.Where(a => a.Id == command.Id).FirstOrDefault();
            if (Student == null)
            {
                return new StudentBaseModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            };
            Student.IsDeleted = true;
            //    _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return new StudentBaseModel
            {
                Data = _mapper.Map<StudentDto>(Student),
                StatusCode = 200,
                Messege = "Data has been Deleted"
            };
        }
    }
}
