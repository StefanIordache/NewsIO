using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IImageService : IGenericDbService
    {
        Task<IEnumerable<Image>> GetAllByNewsIdAsync(int newsId);
    }
}
