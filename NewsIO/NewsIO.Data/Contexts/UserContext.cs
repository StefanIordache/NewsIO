using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Contexts
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        virtual public void CreateUserModel(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreateUserModel(ref modelBuilder);
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
