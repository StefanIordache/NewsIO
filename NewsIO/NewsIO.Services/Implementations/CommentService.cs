using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections;
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

        public async Task<IEnumerable<Comment>> GetAllByNewsIdAsync(int newsId)
        {
            return await Context.Set<Comment>().Where(c => c.NewsId == newsId).ToListAsync();
        }

        public async Task<(IEnumerable<Comment>, int)> GetWithPaginationByNewsIdAsync(int newsId, int pageSize, int pageNo)
        {
            if (pageSize > 0)
            {
                int offset = (pageNo - 1) * pageSize;

                int totalNoOfEntries = CountEntries<Comment>();

                if (offset > totalNoOfEntries)
                {
                    return (null, 0);
                }

                IEnumerable<Comment> returnList;

                if (totalNoOfEntries < offset + pageSize)
                {
                    returnList = await Context.Set<Comment>().Where(c => c.NewsId == newsId).Skip(offset).Take(totalNoOfEntries - offset).ToListAsync();
                }
                else
                {
                    returnList = await Context.Set<Comment>().Where(c => c.NewsId == newsId).Skip(offset).Take(pageSize).ToListAsync();
                }

                return (returnList, totalNoOfEntries);
            }

            return (null, 0);
        }
    }
}
