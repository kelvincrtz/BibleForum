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

        IEnumerable<PostReply> GetAll();
        IEnumerable<PostReplyReply> GetAllPostReplies(int postReplyId);
        IEnumerable<PostReplySupportingBibleVerse> GetAllSupportVersePostReplies(int postReplyId);

        Task Delete(int id);
        Task DeleteAllReplies(int postReplyId);
        Task EditPostReplyContent(int id, string newContent);

        Task Vote(int id);

        Task Add(PostReplyReply replyReply);
    }
}
