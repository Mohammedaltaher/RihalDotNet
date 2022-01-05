using System.Collections.Generic;
using Application.Features.StudentFeatures.Commands;
using Application.Features.StudentFeatures.Queries;
using Domain.Entities;

namespace WebApi.Test;
public static class StudentData
{
    public static List<Student> MockStudentSamples()
    {
        var Student = new List<Student>()
            {
                new Student()
                {
                    Name = "Student2",
                },
                new Student()
                {
                    Name = "Student",
                }
            };

        return Student;
    }
    public static GetStudentByIdQuery MockGetStudentByIdQuery()
    {
        return new GetStudentByIdQuery()
        {
            Id = 1
        };
    }
    public static CreateStudentCommand MockCreateStudentCommand()
    {
        return new CreateStudentCommand()
        {
            Name = "Student2",
        };
    }
    public static UpdateStudentCommand MockUpdateStudentCommand()
    {
        return new UpdateStudentCommand()
        {
            Id = 1,
            Name = "Student25",
        };
    }
    public static DeleteStudentByIdCommand MockDeleteStudentByIdCommand()
    {
        return new DeleteStudentByIdCommand()
        {
            Id = 1
        };
    }

}
