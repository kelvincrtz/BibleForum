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
    public class PostReplyReplyService : IPostReplyReply
    {
        private readonly ApplicationDbContext _dbContext;

        public PostReplyReplyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(PostReplyReply replyReply)
        {

            _dbContext.Add(replyReply);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var postReplyReply = GetById(id);

            _dbContext.Remove(postReplyReply);

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPostReplyContent(int id, string newContent)
        {
            var postReplyReply = GetById(id);

            _dbContext.Update(postReplyReply);

            postReplyReply.Content = newContent;
            postReplyReply.IsEdited = true;
            postReplyReply.EditedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<PostReplyReply> GetAll()
        {
            return _dbContext.PostReplyReplies
                .Include(post => post.Post)
                .Include(postReply => postReply.PostReply)
                .Include(user => user.User);
        }

        public PostReplyReply GetById(int id)
        {
            return _dbContext.PostReplyReplies
                .Where(rreply => rreply.Id == id)
                .Include(post => post.Post)
                .Include(postReply => postReply.PostReply)
                .Include(user => user.User)
                .First();
        }

        public Task Vote(int id)
        {
            throw new NotImplementedException();
        }
    }
}
