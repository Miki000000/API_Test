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
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Stock>()
            .HasMany(s => s.Comments)
            .WithOne(c => c.Stock)
            .HasForeignKey(c => c.StockId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Portfolio>(
            x => x.HasKey(p => new { p.AppUserId, p.StockId })
        );
        modelBuilder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);
        modelBuilder.Entity<Portfolio>()
            .HasOne(p => p.Stock)
            .WithMany(s => s.Portfolios)
            .HasForeignKey(p => p.StockId);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole{ Name = "User", NormalizedName = "USER"}
        };
        modelBuilder.Entity<IdentityRole>()
            .HasData(roles);
    }
}
