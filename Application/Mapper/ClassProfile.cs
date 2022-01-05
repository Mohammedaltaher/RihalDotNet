using Application.Features.ClassFeatures.Commands;
using Application.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper;
public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<Class, CreateClassCommand>();
        CreateMap<CreateClassCommand, Class>();
        CreateMap<UpdateClassCommand, Class>();
        CreateMap<Class, ClassDto>();
    }
}
