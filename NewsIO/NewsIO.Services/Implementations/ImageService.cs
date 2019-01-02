using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class ImageService : GenericDbService, IImageService
    {
        public ImageService(ApplicationContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Image>> GetAllByNewsIdAsync(int newsId)
        {
            return await Context.Set<Image>().Where(i => i.NewsId == newsId).ToListAsync();
        }
    }
}
