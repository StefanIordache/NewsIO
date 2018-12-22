using NewsIO.Data.Models.Account;
using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils
{
    public class SeedHelper
    {
        private static IDictionary<User, string> usersSeed = new Dictionary<User, string>();

        private static ICollection<Category> categoriesSeed = new List<Category>();

        public SeedHelper()
        {
            PopulateSeed();
        }

        public static IDictionary<User, string> GetUsers() => usersSeed;

        public static ICollection<Category> GetCategories() => categoriesSeed;

        private void PopulateSeed()
        {
            //Seed users
            PopulateUsers();

            //Seed categories
            PopulateCategories();
            
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
                Email = "moderator@test.com",
                UserName = "moderator",
                FirstName = "Test",
                LastName = "Moderator",
            }, "Moderator");
            usersSeed.Add(new User
            {
                Email = "admin@test.com",
                UserName = "admin",
                FirstName = "Test",
                LastName = "Admin",
            }, "Administrator");
        }

        private void PopulateCategories()
        {
            categoriesSeed.Add(new Category { Title = "Sport", Description = "Sport category" });
            categoriesSeed.Add(new Category { Title = "Politics", Description = "Politics category" });
            categoriesSeed.Add(new Category { Title = "IT&C", Description = "IT&C category" });
        }
    }
}
