using NewsIO.Data.Contexts;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Services.Implementations
{
    class NewsRequestService : GenericDbService, INewsRequestService
    {
        public NewsRequestService(ApplicationContext context)
            : base(context)
        { }
    }
}
