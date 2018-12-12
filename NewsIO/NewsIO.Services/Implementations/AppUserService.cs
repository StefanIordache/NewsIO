using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models;
using NewsIO.Data.Models.User;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class AppUserService : GenericDbService, IAppUserService
    {
        public AppUserService(UserContext context)
            : base(context)
        { }

        public override async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var returnList = await Context.Set<AppUser>().Include(ap => ap.Identity).ToListAsync();
            return (IEnumerable<T>)returnList;
        }

        public override async Task<(IEnumerable<T>, int)> GetWithPaginationAsync<T>(int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<AppUser>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<AppUser> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context.Set<AppUser>().Include(ap => ap.Identity).Skip(offset).Take(totalNoOfEntries - offset).ToListAsync();
                }
                else
                {
                    returnList = await Context.Set<AppUser>().Include(ap => ap.Identity).Skip(offset).Take(pageSize).ToListAsync();
                }

                return ((IEnumerable<T>)returnList, totalNoOfEntries);
            }

            return (null, 0);
        }
    }
}
