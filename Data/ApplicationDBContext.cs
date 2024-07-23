using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Models;
using ApiTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
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

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole{ Name = "User", NormalizedName = "USER"}
        };
        modelBuilder.Entity<IdentityRole>()
            .HasData(roles);
    }
}
