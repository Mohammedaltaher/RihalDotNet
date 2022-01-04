using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        modelBuilder.Entity<Class>().HasData(
        new Class { Id = 1, Name = "First" },
        new Class { Id = 2, Name = "Second" }
        );
        modelBuilder.Entity<Student>().HasData(
          new Student { Id = 1, Name = "Ahmed", Birth_Date = DateTime.Now, ClassId = 1 , CountryId = 1},
          new Student { Id = 2, Name = "Ali", Birth_Date = DateTime.Now, ClassId = 1 , CountryId = 2 }
          );
        modelBuilder.Entity<Country>().HasData(
         new Country { Id = 1, Name = "Sudan" },
         new Country { Id = 2, Name = "UAE" }
         );
       
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

