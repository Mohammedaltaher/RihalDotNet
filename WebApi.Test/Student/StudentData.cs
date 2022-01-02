using System.Collections.Generic;
using Application.Features.StudentFeatures.Commands;
using Application.Features.StudentFeatures.Queries;
using Domain.Entities;

namespace WebApi.Test.Settings;
public static class StudentData
{
    public static List<Student> MockStudentSamples()
    {
        var Student = new List<Student>()
            {
                new Student()
                {
                    Name = "Payment2",
                },
                new Student()
                {
                    Name = "Payment",
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
            Name = "Payment2",
        };
    }
    public static UpdateStudentCommand MockUpdateStudentCommand()
    {
        return new UpdateStudentCommand()
        {
            Id = 1,
            Name = "Payment25",
        };
    }
    public static DeleteStudentByIdCommand MockDeleteStudentByIdCommand()
    {
        return new DeleteStudentByIdCommand()
        {
            Id = 2
        };
    }

}
