using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class NewsService : GenericDbService, INewsService   
    {
        public NewsService(ApplicationContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<News>> GetAlphabeticalByCategoryAsync(Category category)
        {
            return await Context
                .Set<News>()
                .Where(n => n.CategoryId == category.Id)
                .OrderBy(n => n.Title)
                .ToListAsync();
        }

        public async Task<(IEnumerable<News>, int)> GetAlphabeticalByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<News>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<News> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderBy(n => n.Title)
                        .Skip(offset)
                        .Take(totalNoOfEntries - offset)
                        .ToListAsync();
                }
                else
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderBy(n => n.Title)
                        .Skip(offset)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0);
        }

        public async Task<IEnumerable<News>> GetLatestAsync()
        {
            return await Context
                .Set<News>()
                .OrderByDescending(n => n.LastEditDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetLatestByCategoryAsync(Category category)
        {
            return await Context
                .Set<News>()
                .Where(n => n.CategoryId == category.Id)
                .OrderByDescending(n => n.LastEditDate)
                .ToListAsync();
        }

        public async Task<(IEnumerable<News>, int)> GetLatestByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<News>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<News> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderByDescending(n => n.LastEditDate)
                        .Skip(offset)
                        .Take(totalNoOfEntries - offset)
                        .ToListAsync();
                }
                else
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderByDescending(n => n.LastEditDate)
                        .Skip(offset)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0); ;
        }

        public async Task<(IEnumerable<News>, int)> GetLatestWithPaginationAsync(int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<News>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<News> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context.Set<News>().OrderByDescending(n => n.LastEditDate).Skip(offset).Take(totalNoOfEntries - offset).ToListAsync();
                }
                else
                {
                    returnList = await Context.Set<News>().OrderByDescending(n => n.LastEditDate).Skip(offset).Take(pageSize).ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0);
        }

        public async Task<IEnumerable<News>> GetNonAlphabeticalByCategoryAsync(Category category)
        {
            return await Context
                .Set<News>()
                .Where(n => n.CategoryId == category.Id)
                .OrderByDescending(n => n.Title)
                .ToListAsync();
        }

        public async Task<(IEnumerable<News>, int)> GetNonAlphabeticalByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<News>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<News> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderByDescending(n => n.Title)
                        .Skip(offset)
                        .Take(totalNoOfEntries - offset)
                        .ToListAsync();
                }
                else
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderByDescending(n => n.Title)
                        .Skip(offset)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0); ;
        }

        public async Task<IEnumerable<News>> GetOldestByCategoryAsync(Category category)
        {
            return await Context
                .Set<News>()
                .Where(n => n.CategoryId == category.Id)
                .OrderBy(n => n.LastEditDate)
                .ToListAsync();
        }

        public async Task<(IEnumerable<News>, int)> GetOldestByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<News>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<News> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderBy(n => n.LastEditDate)
                        .Skip(offset)
                        .Take(totalNoOfEntries - offset)
                        .ToListAsync();
                }
                else
                {
                    returnList = await Context
                        .Set<News>()
                        .Where(n => n.CategoryId == category.Id)
                        .OrderBy(n => n.LastEditDate)
                        .Skip(offset)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0);
        }
    }
}
