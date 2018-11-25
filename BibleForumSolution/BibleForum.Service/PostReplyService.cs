using BibleForum.Data;
using BibleForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Service
{
    public class PostReplyService : IPostReply
    {
        private readonly ApplicationDbContext _dbContext;

        public PostReplyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostReplyContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostReply> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostReply GetById(int id)
        {
            return _dbContext.PostReplies.Where(postReply => postReply.Id == id)
                .Include(u => u.User)
                .FirstOrDefault();
        }
    }
}
