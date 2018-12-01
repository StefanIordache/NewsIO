using NewsIO.Data.Contexts;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Services.Implementations
{
    public class CommentService : GenericDbService, ICommentService
    {
        public CommentService(ApplicationContext context)
            : base(context)
        { }
    }
}
