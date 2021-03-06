﻿using BibleForum.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.ReplyLike
{
    public class PostReplyLikeModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime Created { get; set; }

        public PostReplyModel PostReply { get; set; }
    }
}
