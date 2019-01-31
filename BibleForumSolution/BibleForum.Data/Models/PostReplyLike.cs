using System;
using System.Collections.Generic;
using System.Text;

namespace BibleForum.Data.Models
{
    public class PostReplyLike
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual PostReply PostReply { get; set; }

    }
}
