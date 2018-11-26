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

        public async Task Delete(int id)
        {
            var postReply = GetById(id);

            _dbContext.Remove(postReply);

            await _dbContext.SaveChangesAsync();

        }

        public async Task EditPostReplyContent(int id, string newContent)
        {
            var postReply = GetById(id);

            _dbContext.Update(postReply);

            postReply.Content = newContent;

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<PostReply> GetAll()
        {
            return _dbContext.PostReplies
                .Include(user => user.User)
                .Include(post => post.Post);
            
        }

        public PostReply GetById(int id)
        {
            return _dbContext.PostReplies.Where(postReply => postReply.Id == id)
                .Include(user => user.User)
                .Include(post => post.Post)
                    .ThenInclude(forum => forum.Forum)
                .First();
        }

        public async Task Vote(int id)
        {
            var num = 1;
            var postReply = GetById(id);

            _dbContext.Update(postReply);

            postReply.VoteCount = num++;

            await _dbContext.SaveChangesAsync();

        }
    }
}
