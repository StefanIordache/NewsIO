using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class NewsRequestService : GenericDbService, INewsRequestService
    { 
        public NewsRequestService(ApplicationContext context)
            : base(context)
        { }

        public override async Task UpdateAsync<NewsRequest>(int id, NewsRequest entry)
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<NewsRequest>(id);

                if (entryToUpdate == null)
                {
                    return;
                }

                foreach (var property in entryToUpdate
                                    .GetType()
                                    .GetProperties((BindingFlags.DeclaredOnly
                                                    | BindingFlags.Public
                                                    | BindingFlags.Instance)))
                {
                    var initialValue = property.GetValue(entryToUpdate);
                    var newValue = property.GetValue(entry);

                    if (!property.Name.Equals("RequestDate") &&
                        !property.Name.Equals("RequestedById") &&
                        !property.Name.Equals("RequestedBy") &&
                        !Equals(initialValue, newValue))
                    {
                        property.SetValue(entryToUpdate, newValue);
                    }
                }



                Context.Set<NewsRequest>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

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
