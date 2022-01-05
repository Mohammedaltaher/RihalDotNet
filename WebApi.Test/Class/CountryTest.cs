using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using Application.Model;
using Application.Features.ClassFeatures.Queries;
using static Application.Features.ClassFeatures.Queries.GetAllClassQuery;
using static Application.Features.ClassFeatures.Commands.CreateClassCommand;
using static Application.Features.ClassFeatures.Queries.GetClassByIdQuery;
using static Application.Features.ClassFeatures.Commands.UpdateClassCommand;
using static Application.Features.ClassFeatures.Commands.DeleteClassByIdCommand;

namespace WebApi.Test;
public class ClassTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public ClassTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.classes).Returns(Fixture.CreateContext().classes);
    }


    [Fact]
    public async Task Can_Get_All_ClasssAsync()
    {
        var handler = new GetAllClasssQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        ClasssBaseModel result = await handler.Handle(new GetAllClassQuery(), CancellationToken.None);
        var Class = result.Data;
        Assert.NotNull(Class);
       
    }


    [Fact]
    public async Task Can_Get_Class_By_IdAsync()
    {
        var handler = new GetClassByIdQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        ClassBaseModel result = await handler.Handle(ClassData.MockGetClassByIdQuery(), CancellationToken.None);
        var Class = result.Data;

        Assert.NotNull(Class.Name);
    }


    [Fact]
    public async Task Can_Add_ClassAsync()
    {
        var handler = new CreateClassCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        ClassBaseModel result = await handler.Handle(ClassData.MockCreateClassCommand(), CancellationToken.None);
        var Class = result.Data;

        Assert.NotNull(Class.Name);
    }
   
    
    [Fact]
    public async Task Can_Update_ClassAsync()
    {
        var handler = new UpdateClassCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        ClassBaseModel result = await handler.Handle(ClassData.MockUpdateClassCommand(), CancellationToken.None);
        var Class = result.Data;

        Assert.NotNull(Class.Name);
    }
  
    
    [Fact]
    public async Task Can_Delete_ClassAsync()
    {
        var handler = new DeleteClassByIdCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        ClassBaseModel result = await handler.Handle(ClassData.MockDeleteClassByIdCommand(), CancellationToken.None);
        var Class = result.Data;

        Assert.NotNull(Class.Name);
    }
}




