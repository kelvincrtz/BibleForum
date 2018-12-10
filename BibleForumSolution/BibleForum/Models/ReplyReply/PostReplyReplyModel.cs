using BibleForum.Models.ReplySupportingBibleVerse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.ReplyReply
{
    public class PostReplyReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }

        public DateTime EditedCreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public int VoteCount { get; set; }

        public int PostId { get; set; }

        public int PostReply { get; set; }
        public string PostReplyContent { get; set; }

        public IEnumerable<PostReplySupportingBibleVerseModel> PostReplySupportingBibleVerses { get; set; }
    }
}
