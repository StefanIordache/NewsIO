using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface INewsService : IGenericDbService
    {
        Task<IEnumerable<News>> GetLatestAsync();

        Task<(IEnumerable<News>, int)> GetLatestWithPaginationAsync(int pageSize, int pageNo);

        Task<IEnumerable<News>> GetLatestByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetLatestByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetAlphabeticalByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetAlphabeticalByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetOldestByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetOldestByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetNonAlphabeticalByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetNonAlphabeticalByCategoryWithPaginationAsync(Category category, int pageSize, int pageNo);
    }
}
