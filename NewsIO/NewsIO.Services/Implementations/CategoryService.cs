using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Application;
using System.Threading.Tasks;
using System.Linq;

namespace NewsIO.Services.Implementations
{
    public class CategoryService : GenericDbService, ICategoryService
    {
        public CategoryService(ApplicationContext context)
            : base(context)
        {
        }

        public Category GetByTitle(string title)
        {
            try
            {
                var category = Context.Set<Category>().FirstOrDefault(c => c.Title == title);

                return category;
            }
            catch
            {
                return null;
            }
        }
    }
}
