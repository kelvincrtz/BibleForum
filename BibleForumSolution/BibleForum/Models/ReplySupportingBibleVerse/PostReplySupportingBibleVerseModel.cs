using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.ReplySupportingBibleVerse
{
    public class PostReplySupportingBibleVerseModel
    {
        public int Id { get; set; }
        public string BibleBook { get; set; }
        public string BibleChapter { get; set; }
        public string BibleVerse { get; set; }
        public string BibleTranslation { get; set; }
        public DateTime Created { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }

        public int PostReplyId { get; set; }

    }
}
