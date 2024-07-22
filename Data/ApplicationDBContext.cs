using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Stock>()
            .HasMany(s => s.Comments)
            .WithOne(c => c.Stock)
            .HasForeignKey(c => c.StockId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
