using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.ReplyReply
{
    public class PostReplyReplyEditModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        [Required]
        [StringLength(50000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Reply Content")]
        public string ReplyContent { get; set; }

        public DateTime EditedCreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public int VoteCount { get; set; }

        public int PostId { get; set; }
        public int PostReplyId { get; set; }
    }
}
