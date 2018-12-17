using BibleForum.Data;
using BibleForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _dbContext;

        public ForumService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Forum forum)
        {
             _dbContext.Add(forum);
             await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _dbContext.Remove(forum);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            var posts = GetById(id).Posts;

            if(posts != null || !posts.Any())
            {
                var postUsers = posts.Select(p => p.User);
                var replyUsers = posts.SelectMany(p => p.Replies).Select(r => r.User);

                return postUsers.Union(replyUsers).Distinct();
            }

            return new List<ApplicationUser>();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _dbContext.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<Forum> GetAllNewTestament()
        {
            return _dbContext.Forums.Include(forum => forum.Posts)
                .Where(forum => forum.OldOrNew == "New")
                .OrderBy(forum => forum.BookOrder);
        }

        public IEnumerable<Forum> GetAllOldTestament()
        {
            return _dbContext.Forums.Include(forum => forum.Posts)
                .Where(forum => forum.OldOrNew == "Old")
                .OrderBy(forum => forum.BookOrder);
        }

        public Forum GetById(int id)
        {
            return _dbContext.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(f 
                    => f.Replies)
                    .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                    .ThenInclude(f => f.User)
                .FirstOrDefault();
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);

            return GetById(id).Posts.Any(post => post.Created > window);
        }

        public Task UpdateForumDescription(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
