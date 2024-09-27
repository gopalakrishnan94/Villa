using System;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }
    public DbSet<Villa> Villas { get; set; }
}
