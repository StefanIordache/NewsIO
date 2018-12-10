using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class CommentService : GenericDbService, ICommentService
    {
        public CommentService(ApplicationContext context)
            : base(context)
        { }

        /*public async Task<(IEnumerable<T>, int)> GetWithPaginationByNewsIdAsync<T>(int newsId, int pageSize, int pageNo) where T : Entity
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
        }*/
    }
}
