using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibleForum.Data
{
    public interface IPostReplyLike
    {
        PostReplyLike GetById(int id);
        IEnumerable<PostReplyLike> GetAll();

        IEnumerable<PostReplyLike> GetByPostReplyId(int postReplyId);
    }
}
