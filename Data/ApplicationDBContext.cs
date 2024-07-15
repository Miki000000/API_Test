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
    public DbSet<StockModel> Stock { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
}
