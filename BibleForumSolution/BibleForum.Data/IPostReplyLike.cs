using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public interface IPostReplyLike
    {
        PostReplyLike GetById(int id);
        IEnumerable<PostReplyLike> GetAll();

        IEnumerable<PostReplyLike> GetLikesByPostReplyId(int postReplyId);

        Task Add(PostReplyLike postReplyLike);
    }
}
