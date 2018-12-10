﻿using BibleForum.Data;
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostReplyContent(int id, string newContent)
        {
            throw new NotImplementedException();
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