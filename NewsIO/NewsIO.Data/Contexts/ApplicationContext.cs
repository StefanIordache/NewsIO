using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<NewsRequest> NewsRequests {get; set;}

        public DbSet<Comment> Comments { get; set; }
    }
}
