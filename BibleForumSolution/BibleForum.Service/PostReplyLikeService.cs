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
    public class PostReplyLikeService : IPostReplyLike
    {
        private ApplicationDbContext _dbContext;

        public PostReplyLikeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(PostReplyLike postReplyLike)
        {
            _dbContext.PostReplyLikes.Add(postReplyLike);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<PostReplyLike> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostReplyLike GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostReplyLike> GetLikesByPostReplyId(int postReplyId)
        {
            return _dbContext.PostReplyLikes.Where(postReplyLike => postReplyLike.PostReply.Id == postReplyId)
                .Include(userLike => userLike.User)
                .Include(postLike => postLike.Post)
                .Include(postReplyLike => postReplyLike.PostReply);
        }
    }
}
