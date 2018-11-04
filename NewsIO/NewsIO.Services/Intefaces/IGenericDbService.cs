using NewsIO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IGenericDbService
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : Entity;

        IQueryable<T> Set<T>() where T : Entity;

        Task<T> GetByIdAsync<T>(int id) where T : Entity;

        Task AddAsync<T>(T entry) where T : Entity;

        Task AddRangeAsync<T>(IEnumerable<T> entries) where T : Entity;

        Task UpdateAsync<T>(int id, T entry) where T : Entity;
    }
}
