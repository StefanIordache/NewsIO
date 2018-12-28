using NewsIO.Data.Contexts;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Services.Implementations
{
    public class NewsService : GenericDbService, INewsService   
    {
        public NewsService(ApplicationContext context)
            : base(context)
        {
        }
    }
}
