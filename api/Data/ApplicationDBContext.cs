using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Region> Region { get; set; }
    public DbSet<Difficulty> Difficulty { get; set; }
    public DbSet<Walk> Walks { get; set; }
    }
}
