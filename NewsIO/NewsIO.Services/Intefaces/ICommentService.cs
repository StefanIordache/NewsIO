using NewsIO.Data.Models;
using NewsIO.Data.Models.Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface ICommentService : IGenericDbService
    {
        Task<(IEnumerable<Comment>, int)> GetWithPaginationByNewsIdAsync(int newsId, int pageSize, int pageNo);

        Task<IEnumerable<Comment>> GetAllByNewsIdAsync(int newsId);
    }
}
