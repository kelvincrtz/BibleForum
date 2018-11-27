using System;
using System.Collections.Generic;
using System.Text;

namespace BibleForum.Data.Models
{
    public class PostReplySupportingBibleVerse
    {
        public int Id { get; set; }
        public string BibleChapter { get; set; }
        public string BibleVerse { get; set; }
        public string BibleTranslation { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
        public virtual PostReply PostReply { get; set; }
    }
}
