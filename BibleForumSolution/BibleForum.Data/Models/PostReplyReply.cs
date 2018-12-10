using System;
using System.Collections.Generic;
using System.Text;

namespace BibleForum.Data.Models
{
    public class PostReplyReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime EditedDate { get; set; }
        public bool IsEdited { get; set; }
        public int VoteCount { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
        public virtual IEnumerable<PostReplySupportingBibleVerse> PostReplySupportingBibleVerse { get; set; }
    }
}
