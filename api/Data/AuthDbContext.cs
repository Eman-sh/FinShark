using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var readerRoleId = "8f4b2d20-7e2d-4c8a-9e0a-2e45a19c0006";
            var writerRoleId = "3a7c8e92-1b4f-4d6e-8c9a-5f2e7b8d4a1c";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = writerRoleId
                },
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = readerRoleId
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}