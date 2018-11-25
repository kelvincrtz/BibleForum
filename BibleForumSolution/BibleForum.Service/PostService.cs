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
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Post post)
        {
            _dbContext.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _dbContext.PostReplies.Add(reply);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = _dbContext.PostReplies.FirstOrDefault(p => p.Id == id);

            _dbContext.Update(post);

            _dbContext.Remove(post);

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPostContent(int id, string newContent)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            _dbContext.Update(post);

            post.Content = newContent;

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _dbContext.Posts
               .Include(post => post.User)
               .Include(post => post.Replies)
                   .ThenInclude(postReplies => postReplies.User)
               .Include(post => post.Forum);
        }

        public Post GetById(int id)
        {
            return _dbContext.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(postReplies => postReplies.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPost(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) ? forum.Posts 
                : forum.Posts.Where(post => post.Title.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFilteredPost(string searchQuery)
        {
            var normalized = searchQuery.ToLower();

            return GetAll().Where(post => post.Title.ToLower().Contains(normalized) || post.Content.ToLower().Contains(normalized));
        }

        public IEnumerable<Post> GetLastestPosts(int n)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _dbContext.Forums.Where(f => f.Id == id).First().Posts;
        }
    }
}
