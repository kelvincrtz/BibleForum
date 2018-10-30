using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Post;
using BibleForum.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;

        public PostController(IPost postService) {
            _postService = postService;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies
            };

            return View(model);
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ImageUrl,
                AuthorName = reply.User.UserName,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                PostId = reply.Post.Id,
                ReplyContent = reply.Content
            });
        }
    }
}