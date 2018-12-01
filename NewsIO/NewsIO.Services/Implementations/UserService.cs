using NewsIO.Data.Contexts;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Services.Implementations
{
    public class UserService : GenericDbService, IUserService
    {
        public UserService(ApplicationContext context)
            : base(context)
        { }
    }
}
