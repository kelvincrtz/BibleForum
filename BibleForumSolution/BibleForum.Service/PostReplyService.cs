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

        public async Task Add(PostReplyReply replyReply)
        {

            _dbContext.PostReplyReplies.Add(replyReply);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var postReply = GetById(id);

            _dbContext.Remove(postReply);

            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAllReplies(int postReplyId)
        {
            IEnumerable<PostReplyReply> replies = GetAllPostReplies(postReplyId);
            IEnumerable<PostReplySupportingBibleVerse> supportVerse = GetAllSupportVersePostReplies(postReplyId);

            if (replies.Any())
            {
                foreach(var replyreplies in replies)
                {
                    _dbContext.Remove(replyreplies);
                }
            }

            if (supportVerse.Any())
            {
                foreach(var supportVerseReply in supportVerse)
                {
                    _dbContext.Remove(supportVerseReply);
                }
            }

            await Delete(postReplyId);

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPostReplyContent(int id, string newContent)
        {
            var postReply = GetById(id);

            _dbContext.Update(postReply);

            postReply.Content = newContent;
            postReply.IsEdited = true;
            postReply.EditedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<PostReply> GetAll()
        {
            return _dbContext.PostReplies
                .Include(user => user.User)
                .Include(post => post.Post);
            
        }

        public IEnumerable<PostReplyReply> GetAllPostReplies(int postReplyId)
        {
            return _dbContext.PostReplyReplies.Where(postReply => postReply.PostReply.Id == postReplyId);
                
        }

        public IEnumerable<PostReplyLike> GetAllPostReplyLikes(int postReplyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostReplySupportingBibleVerse> GetAllSupportVersePostReplies(int postReplyId)
        {
            return _dbContext.PostReplySupportingBibleVerses.Where(postReply => postReply.PostReply.Id == postReplyId);
        }

        public PostReply GetById(int id)
        {
            return _dbContext.PostReplies.Where(postReply => postReply.Id == id)
                .Include(user => user.User)
                .Include(post => post.Post)
                    .ThenInclude(forum => forum.Forum)
                .Include(supportVerse => supportVerse.PostReplySupportingBibleVerse)
                .Include(replyreply => replyreply.PostReplyReply)
                .Include(postLikes => postLikes.PostReplyLikes)
                .First();
        }

        public async Task Vote(int id)
        {
            var postReply = GetById(id);

            _dbContext.Update(postReply);

            postReply.VoteCount += 1;

            await _dbContext.SaveChangesAsync();

        }
    }
}
