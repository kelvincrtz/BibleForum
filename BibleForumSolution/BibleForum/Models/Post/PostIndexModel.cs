﻿using BibleForum.Models.Reply;
using BibleForum.Models.ReplySupportingBibleVerse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.Post
{
    public class PostIndexModel

    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string AuthorImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public DateTime Created { get; set; }
        public string PostContent { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string ForumImage { get; set; }

        public DateTime EditedCreated { get; set; }
        public bool IsEdited { get; set; }

        public IEnumerable<PostReplyModel> Replies { get; set; }
        public IEnumerable<PostReplySupportingBibleVerseModel> SupportingVerse { get; set; }

    }
}
