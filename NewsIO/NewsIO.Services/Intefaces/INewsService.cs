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

        Task<(IEnumerable<News>, int)> GetLatestWithPaginationAsync<T>(int pageSize, int pageNo);

        Task<IEnumerable<News>> GetLatestByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetLatestByCategoryWithPaginationAsync<T>(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetAlpabeticalByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetAlphabeticalByCategoryWithPaginationAsync<T>(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetOldestByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetOldestByCategoryWithPaginationAsync<T>(Category category, int pageSize, int pageNo);

        Task<IEnumerable<News>> GetNonAlpabeticalByCategoryAsync(Category category);

        Task<(IEnumerable<News>, int)> GetNonAlphabeticalByCategoryWithPaginationAsync<T>(Category category, int pageSize, int pageNo);
    }
}
