using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Student> students { get; set; }
    DbSet<Class> classes { get; set; }
    DbSet<Country> countries { get; set; }
    Task<int> SaveChangesAsync();
}

