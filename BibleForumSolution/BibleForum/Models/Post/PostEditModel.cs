using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.Post
{
    public class PostEditModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreated { get; set; }

        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }

        public DateTime EditedDate { get; set; }
        public bool IsEdited { get; set; }
    }
}
