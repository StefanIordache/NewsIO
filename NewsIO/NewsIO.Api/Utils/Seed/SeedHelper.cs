using Microsoft.AspNetCore.Identity;
using NewsIO.Data.Models.Account;
using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils.Seed
{
    public class SeedHelper
    {
        private static ICollection<string> rolesSeed = new List<string>();

        private static IDictionary<User, string> usersSeed = new Dictionary<User, string>();

        private static ICollection<Category> categoriesSeed = new List<Category>();

        private static IDictionary<NewsRequest, string> newsRequestsSeed = new Dictionary<NewsRequest, string>();

        public SeedHelper()
        {
            PopulateSeed();
        }

        public static ICollection<string> GetRoles() => rolesSeed;

        public static IDictionary<User, string> GetUsers() => usersSeed;

        public static ICollection<Category> GetCategories() => categoriesSeed;

        public static IDictionary<NewsRequest, string> GetNewsRequests() => newsRequestsSeed;

        private void PopulateSeed()
        {
            PopulateRoles();

            PopulateUsers();

            PopulateCategories();

            PopulateNewsRequests();    
        }

        private void PopulateRoles()
        {
            rolesSeed.Add("Administrator");
            rolesSeed.Add("Moderator");
            rolesSeed.Add("Member");
        }

        private void PopulateUsers()
        {
            usersSeed.Add(new User
            {
                Email = "member@test.com",
                UserName = "member",
                FirstName = "Test",
                LastName = "Member",
            }, "Member");
            usersSeed.Add(new User
            {
                Email = "member1@test.com",
                UserName = "member1",
                FirstName = "Test",
                LastName = "Member1",
            }, "Member");
            usersSeed.Add(new User
            {
                Email = "moderator@test.com",
                UserName = "moderator",
                FirstName = "Test",
                LastName = "Moderator",
            }, "Moderator");
            usersSeed.Add(new User
            {
                Email = "moderator1@test.com",
                UserName = "moderator1",
                FirstName = "Test",
                LastName = "Moderator1",
            }, "Moderator");
            usersSeed.Add(new User
            {
                Email = "admin@test.com",
                UserName = "admin",
                FirstName = "Test",
                LastName = "Admin",
            }, "Administrator");
            usersSeed.Add(new User
            {
                Email = "admin1@test.com",
                UserName = "admin1",
                FirstName = "Test",
                LastName = "Admin1",
            }, "Administrator");
        }

        private void PopulateCategories()
        {
            categoriesSeed.Add(new Category { Title = "Sport", Description = "Sport category" });
            categoriesSeed.Add(new Category { Title = "Politics", Description = "Politics category" });
            categoriesSeed.Add(new Category { Title = "IT&C", Description = "IT&C category" });
        }

        private void PopulateNewsRequests()
        {
            newsRequestsSeed.Add(new NewsRequest
            {
                Title = "Reduceri la electrocasnice",
                Description = "Iar a venit Black-Friday",
                IsClosed = false,
                Status = "New"
            }, "IT&C");
            newsRequestsSeed.Add(new NewsRequest
            {
                Title = "Gigi a dat gol cu degetul mic de la piciorul stang",
                Description = "Soc! Soc! Soc!",
                IsClosed = true,
                Status = "FakeNews! A dat autogol"
            }, "Sport");
            newsRequestsSeed.Add(new NewsRequest
            {
                Title = "Microsoft a dat faliment",
                Description = "Minunea s-a intamplat",
                IsClosed = true,
                Status = "Fake News! Am vrea noi"
            }, "IT&C");
            newsRequestsSeed.Add(new NewsRequest
            {
                Title = "Dragnea a murit",
                Description = "Si Dancila a murit",
                IsClosed = false,
                Status = "New"
            }, "Politics");
        }
    }
}
