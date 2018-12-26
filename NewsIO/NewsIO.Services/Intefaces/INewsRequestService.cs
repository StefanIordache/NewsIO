using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface INewsRequestService : IGenericDbService
    {
        Task<bool> CloseNewsRequestAsync(NewsRequest newsRequest);

        Task<bool> OpenNewsRequestAsync(NewsRequest newsRequest);

        Task<bool> ChangeNewsRequestCategoryAsync(NewsRequest newsRequest, Category category);
    }
}
