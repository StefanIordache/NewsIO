﻿using NewsIO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IGenericDbService
    {
        int CountEntries<T>() where T : Entity;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : Entity;

        Task<(IEnumerable<T>, int)> GetWithPaginationAsync<T>(int pageSize, int pageNo) where T : Entity;

        IQueryable<T> Set<T>() where T : Entity;

        Task<T> GetByIdAsync<T>(int id) where T : Entity;

        Task<int> AddAsync<T>(T entry) where T : Entity;

        Task AddRangeAsync<T>(IEnumerable<T> entries) where T : Entity;

        Task UpdateAsync<T>(int id, T entry) where T : Entity;

        Task Delete<T>(int id) where T : Entity;

        Task PublishEntity<T>(int entryId, string userId, string userName) where T : Entity;

        Task UpdateLastEdit<T>(int entryId, string userId, string userName) where T : Entity;

        Task DeletePublisherId<T>(int entryId) where T : Entity;

        Task DeleteLastEditorId<T>(int entryId) where T : Entity;
    }
}
