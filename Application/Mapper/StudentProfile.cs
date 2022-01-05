using Application.Features.StudentFeatures.Commands;
using Application.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper;
public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, CreateStudentCommand>();
        CreateMap<CreateStudentCommand, Student>();

        CreateMap<UpdateStudentCommand, Student>();

        CreateMap<Student, StudentDto>()
            .ForMember(from => from.ClassName, to => to.MapFrom(value => value.Class.Name))
            .ForMember(from => from.CountryName, to => to.MapFrom(value => value.Country.Name));

        CreateMap<Student, StudentsPerCountryDto>()
           .ForMember(from => from.CountryName, to => to.MapFrom(value => value.Class.Name))
           .ForMember(from => from.NoStudents, to => to.MapFrom(value => value.Country.Name));
    }
}
