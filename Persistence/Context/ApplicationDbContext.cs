using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Persistence.Context;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    
    {
        Database.EnsureCreated();
        if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        {
            //Database.Migrate();
        }
        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Class>().HasData(DataFaker.GetClassesData());
        modelBuilder.Entity<Country>().HasData(DataFaker.GetCountriesData());
        modelBuilder.Entity<Student>().HasData(DataFaker.GetStudentsData());
       
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        UpdateUpdateDate();
        return await base.SaveChangesAsync();
    }

    private void UpdateUpdateDate()
    {
        var UpdateDate = "UpdateDate";
        ChangeTracker.DetectChanges();
        var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (EntityEntry entity in modified)
        {
            foreach (PropertyEntry prop in entity.Properties)
            {
                if (prop.Metadata.Name == UpdateDate)
                {
                    entity.CurrentValues[UpdateDate] = DateTime.Now;
                }
            }
        }
    }

    #region DbSet Properties
    public DbSet<Student> students { get; set; }
    public DbSet<Class> classes { get; set; }
    public DbSet<Country> countries { get; set; }

    #endregion


}

