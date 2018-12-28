using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class NewsRequestService : GenericDbService, INewsRequestService
    { 
        public NewsRequestService(ApplicationContext context)
            : base(context)
        { }

        public async Task<bool> CloseNewsRequestAsync(NewsRequest newsRequest)
        {
            try
            {
                newsRequest.IsClosed = true;

                Context.Set<NewsRequest>().Update(newsRequest);
                await Context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> OpenNewsRequestAsync(NewsRequest newsRequest)
        {
            try
            {
                newsRequest.IsClosed = false;

                Context.Set<NewsRequest>().Update(newsRequest);
                await Context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangeNewsRequestCategoryAsync(NewsRequest newsRequest, Category category)
        {
            try
            {
                newsRequest.Category = category;

                Context.Set<NewsRequest>().Update(newsRequest);

                await Context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
