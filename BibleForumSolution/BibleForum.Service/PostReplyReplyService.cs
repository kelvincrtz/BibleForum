using BibleForum.Data;
using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Service
{
    public class PostReplyReplyService : IPostReplyReply
    {


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
            throw new NotImplementedException();
        }

        public PostReplyReply GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Vote(int id)
        {
            throw new NotImplementedException();
        }
    }
}
