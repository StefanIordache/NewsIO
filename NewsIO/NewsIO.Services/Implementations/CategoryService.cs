using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using NewsIO.Data.Contexts;

namespace NewsIO.Services.Implementations
{
    public class CategoryService : GenericDbService, ICategoryService
    {
        public CategoryService(ApplicationContext context)
            : base(context)
        { }
    }
}
