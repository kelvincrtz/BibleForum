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

        public IEnumerable<Forum> GetAll()
        {
            return _dbContext.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            return _dbContext.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(f => f.Replies)
                    .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                    .ThenInclude(f => f.User)
                .FirstOrDefault();
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
