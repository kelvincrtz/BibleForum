using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public interface IPostReplyReply
    {
        PostReplyReply GetById(int id);

        IEnumerable<PostReplyReply> GetAll();

        Task Delete(int id);
        Task EditPostReplyContent(int id, string newContent);

        Task Vote(int id);
    }
}
