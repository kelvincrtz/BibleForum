using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public interface IPostReply
    {
        PostReply GetById(int id);
        string GetContentById(int id);

        IEnumerable<PostReply> GetAll();

        Task Delete(int id);
        Task EditPostReplyContent(int id, string newContent);

    }
}
