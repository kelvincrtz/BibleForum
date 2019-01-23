using BibleForum.Models.ReplyReply;
using BibleForum.Models.ReplySupportingBibleVerse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime Created { get; set; }

        [Required]
        [StringLength(50000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string ReplyContent { get; set; }

        public DateTime EditedCreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public int VoteCount { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }
        public int ForumId { get; set; }

        public IEnumerable<PostReplySupportingBibleVerseModel> PostReplySupportingBibleVerses { get; set; }
        public IEnumerable<PostReplyReplyModel> PostReplyReplies { get; set; }

    }
}
