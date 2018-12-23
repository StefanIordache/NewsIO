using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Models;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class GenericDbService : IGenericDbService
    {
        protected DbContext Context { get; set; }

        public GenericDbService(DbContext context)
        {
            Context = context;
        }

        public virtual int CountEntries<T>() where T : Entity
        {
            return Context.Set<T>().Count();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync<T>() where T : Entity
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<(IEnumerable<T>, int)> GetWithPaginationAsync<T>(int pageSize, int pageNo) where T : Entity
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<T>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<T> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context.Set<T>().Skip(offset).Take(totalNoOfEntries - offset).ToListAsync();
                }
                else
                {
                    returnList = await Context.Set<T>().Skip(offset).Take(pageSize).ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0);         
        }

        public virtual IQueryable<T> Set<T>() where T : Entity
        {
            return Context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : Entity
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<int> AddAsync<T>(T entry) where T : Entity
        {
            await Context.Set<T>().AddAsync(entry);
            await Context.SaveChangesAsync();

            return entry.Id;
        }

        public virtual async Task AddRangeAsync<T>(IEnumerable<T> entries) where T : Entity
        {
            await Context.Set<T>().AddRangeAsync(entries);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync<T>(int id, T entry) where T : Entity
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<T>(id);
                
                if (entryToUpdate == null)
                {
                    return;
                }

                foreach(var property in entryToUpdate
                                    .GetType()
                                    .GetProperties((BindingFlags.DeclaredOnly 
                                                    | BindingFlags.Public 
                                                    | BindingFlags.Instance)))
                {
                    var initialValue = property.GetValue(entryToUpdate);
                    var newValue = property.GetValue(entry);

                    if (!Equals(initialValue, newValue))
                    {
                        property.SetValue(entryToUpdate, newValue);
                    }
                }

                Context.Set<T>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public virtual async Task PublishEntity<T>(int entryId, string userId, string userName) where T : Entity
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<T>(entryId);

                if (entryToUpdate ==  null)
                {
                    return;
                }

                var currentTime = DateTime.Now;

                entryToUpdate.PublishDate = currentTime;
                entryToUpdate.PublishedBy = userName;
                entryToUpdate.PublishedById = userId;

                entryToUpdate.LastEditDate = currentTime;
                entryToUpdate.LastEditedBy = userName;
                entryToUpdate.LastEditedyById = userId;

                Context.Set<T>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public virtual async Task UpdateLastEdit<T>(int entryId, string userId, string userName) where T : Entity
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<T>(entryId);

                if (entryToUpdate == null)
                {
                    return;
                }

                var currentTime = DateTime.Now;

                entryToUpdate.LastEditDate = currentTime;
                entryToUpdate.LastEditedBy = userName;
                entryToUpdate.LastEditedyById = userId;

                Context.Set<T>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public virtual async Task DeletePublisherId<T>(int entryId) where T : Entity
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<T>(entryId);

                if (entryToUpdate == null)
                {
                    return;
                }

                entryToUpdate.PublishedById = string.Empty;

                Context.Set<T>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public virtual async Task DeleteLastEditorId<T>(int entryId) where T : Entity
        {
            try
            {
                var entryToUpdate = await GetByIdAsync<T>(entryId);

                if (entryToUpdate == null)
                {
                    return;
                }

                entryToUpdate.LastEditedyById = string.Empty;

                Context.Set<T>().Update(entryToUpdate);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public virtual async Task Delete<T>(int entryId) where T : Entity
        {
            try
            {
                var entryToDelete = await GetByIdAsync<T>(entryId);

                Context.Remove(entryToDelete);

                await Context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }
    }
}
