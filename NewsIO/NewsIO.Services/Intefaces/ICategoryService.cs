using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface ICategoryService : IGenericDbService
    {
        Category GetByTitle(string title);
    }
}
