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

        private static IDictionary<News, string> newsSeed = new Dictionary<News, string>();

        public SeedHelper()
        {
            PopulateSeed();
        }

        public static ICollection<string> GetRoles()
        {
            if (!rolesSeed.Any())
            {
                PopulateRoles();
            }

            return rolesSeed;
        }

        public static IDictionary<User, string> GetUsers()
        {
            if (!usersSeed.Any())
            {
                PopulateUsers();
            }

            return usersSeed;
        }

        public static ICollection<Category> GetCategories()
        {
            if (!categoriesSeed.Any())
            {
                PopulateCategories();
            }

            return categoriesSeed;
        }

        public static IDictionary<NewsRequest, string> GetNewsRequests()
        {
            if (!newsRequestsSeed.Any())
            {
                PopulateNewsRequests();
            }

            return newsRequestsSeed;
        }

        public static IDictionary<News, string> GetNews()
        {
            if (!newsSeed.Any())
            {
                PopulateNews();
            }

            return newsSeed;
        }

        private void PopulateSeed()
        {
            PopulateRoles();

            PopulateUsers();

            PopulateCategories();

            PopulateNewsRequests();    
        }

        private static void PopulateRoles()
        {
            rolesSeed.Add("Administrator");
            rolesSeed.Add("Moderator");
            rolesSeed.Add("Member");
        }

        private static void PopulateUsers()
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

        private static void PopulateCategories()
        {
            categoriesSeed.Add(new Category { Title = "Sport", Description = "Sport category" });
            categoriesSeed.Add(new Category { Title = "Politics", Description = "Politics category" });
            categoriesSeed.Add(new Category { Title = "IT&C", Description = "IT&C category" });
            categoriesSeed.Add(new Category { Title = "Din popor", Description = "De toate" });
        }

        private static void PopulateNewsRequests()
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

        protected static void PopulateNews()
        {
            newsSeed.Add(new News
            {
                Title = "Dragnea fura",
                Headline = "Ne mai mira ceva?",
                Content = "El comandante PSD a furat 3 oua de strut",
                ThumbnailUrl = null,
                ExternalUrl = null,
                FromRequest = false,
                NewsRequest = null
            }, "Politics");
            newsSeed.Add(new News
            {
                Title = "Droguri legalizate",
                Headline = "Da! Da! Da!",
                Content = "Nu dati banii pe prostii, luati Canabis la copii",
                ThumbnailUrl = null,
                ExternalUrl = null,
                FromRequest = false,
                NewsRequest = null
            }, "Din popor");
            newsSeed.Add(new News
            {
                Title = "Cea mai mare sarma din lume",
                Headline = "Romania detine cea mai mare sarma din lume",
                Content = "O sarma de 4 metri lungime a fost conceputa in orasul Baicoi",
                ThumbnailUrl = null,
                ExternalUrl = null,
                FromRequest = false,
                NewsRequest = null
            }, "Din popor");
            newsSeed.Add(new News
            {
                Title = "Hockey de senzatie",
                Headline = "Bicicleta a intra direct in poarta",
                Content = "Simona Halep a marcat cel mai frumos gol din istoria baschetului ploiestean.",
                ThumbnailUrl = null,
                ExternalUrl = null,
                FromRequest = false,
                NewsRequest = null,
            }, "Sport");
        }
    }
}
