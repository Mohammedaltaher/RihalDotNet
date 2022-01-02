using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using WebApi.Test.Settings;

namespace WebApi.Test;
public class SharedDatabaseFixture 
{
    private static readonly object _lock = new object();
    private static bool _databaseInitialized;
    public ApplicationDbContext? Context { get; set; }

    public SharedDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(StudentData.MockStudentSamples());
                    context.SaveChanges();
                }
                _databaseInitialized = true;
            }
        }
    }

    public ApplicationDbContext CreateContext(DbTransaction? transaction = null)
    {
        var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("test")
            .Options);

        if (transaction != null)
        {
            context.Database.UseTransaction(transaction);
        }
        return context;
    }
}