using NewsIO.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface ICommentService : IGenericDbService
    {
        //Task<(IEnumerable<T>, int)> GetWithPaginationByNewsIdAsync<T>(int newsId, int pageSize, int pageNo) where T : Entity;
    }
}
