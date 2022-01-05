using System.Collections.Generic;
using Application.Features.ClassFeatures.Commands;
using Application.Features.ClassFeatures.Queries;
using Domain.Entities;

namespace WebApi.Test;
public static class ClassData
{
    public static List<Class> MockClassSamples()
    {
        var Class = new List<Class>()
            {
                new Class()
                {
                    Name = "Class2",
                },
                new Class()
                {
                    Name = "Class",
                }
            };

        return Class;
    }
    public static GetClassByIdQuery MockGetClassByIdQuery()
    {
        return new GetClassByIdQuery()
        {
            Id = 1
        };
    }
    public static CreateClassCommand MockCreateClassCommand()
    {
        return new CreateClassCommand()
        {
            Name = "Class2",
        };
    }
    public static UpdateClassCommand MockUpdateClassCommand()
    {
        return new UpdateClassCommand()
        {
            Id = 1,
            Name = "Class25",
        };
    }
    public static DeleteClassByIdCommand MockDeleteClassByIdCommand()
    {
        return new DeleteClassByIdCommand()
        {
            Id = 1
        };
    }

}
