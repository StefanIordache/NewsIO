﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Contexts
{
    public class UserContext : IdentityDbContext<User, UserRole, int>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        virtual public void CreateUserModel(ref ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().Ignore(t => t.Roles)
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //CreateUserModel(ref modelBuilder);
        }
    }
}
