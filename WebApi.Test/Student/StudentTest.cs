using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using Application.Model;
using Application.Features.StudentFeatures.Queries;
using static Application.Features.StudentFeatures.Queries.GetAllStudentQuery;
using static Application.Features.StudentFeatures.Commands.CreateStudentCommand;
using static Application.Features.StudentFeatures.Queries.GetStudentByIdQuery;
using static Application.Features.StudentFeatures.Commands.UpdateStudentCommand;
using static Application.Features.StudentFeatures.Commands.DeleteStudentByIdCommand;

namespace WebApi.Test.Settings;
public class StudentTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public StudentTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.students).Returns(Fixture.CreateContext().students);
    }


    [Fact]
    public async Task Can_Get_All_StudentsAsync()
    {
        var handler = new GetAllStudentsQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        StudentsBaseModel result = await handler.Handle(new GetAllStudentQuery(), CancellationToken.None);
        var Student = result.Data;
        Assert.NotNull(Student);
        Assert.Equal(StudentData.MockStudentSamples()[0].Name, Student[0].Name);
        Assert.Equal(2, Student.Count);
       
    }


    [Fact]
    public async Task Can_Get_Student_By_IdAsync()
    {
        var handler = new GetStudentByIdQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        StudentBaseModel result = await handler.Handle(StudentData.MockGetStudentByIdQuery(), CancellationToken.None);
        var Student = result.Data;

        Assert.Equal("Payment2", Student.Name);
    }


    [Fact]
    public async Task Can_Add_StudentAsync()
    {
        var handler = new CreateStudentCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        StudentBaseModel result = await handler.Handle(StudentData.MockCreateStudentCommand(), CancellationToken.None);
        var Student = result.Data;

        Assert.Equal("Payment2", Student.Name);
    }
   
    
    [Fact]
    public async Task Can_Update_StudentAsync()
    {
        var handler = new UpdateStudentCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        StudentBaseModel result = await handler.Handle(StudentData.MockUpdateStudentCommand(), CancellationToken.None);
        var Student = result.Data;

        Assert.Equal("Payment25", Student.Name);
    }
  
    
    [Fact]
    public async Task Can_Delete_StudentAsync()
    {
        var handler = new DeleteStudentByIdCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        StudentBaseModel result = await handler.Handle(StudentData.MockDeleteStudentByIdCommand(), CancellationToken.None);
        var Student = result.Data;

        Assert.Equal("Payment", Student.Name);
    }
}




