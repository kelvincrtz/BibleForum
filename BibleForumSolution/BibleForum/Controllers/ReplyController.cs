using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly IPostReply _postReplyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public ReplyController(IPost postService, IPostReply postReplyService, UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postService = postService;
            _postReplyService = postReplyService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task <IActionResult> Create(int id)
        {
            //Get post and track for reply
            var post = _postService.GetById(id);

            //Get the application user that will write the reply for this post
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),
                
                OriginalPostWritter = post.User.UserName,

                ForumName = post.Forum.Title,
                ForumId = post.Forum.Id,
                ForumImageUrl = post.Forum.ImageUrl,

                Created = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await _postService.AddReply(reply);


            //Raise the rating of the user
            await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        private PostReply BuildReply(PostReplyModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);

            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
                // id from the PostReplies data table is automated - No need to wire one for it
            };
        }

        //Edit 
        public async Task<IActionResult> Edit(int id)
        {
            //Get post and track for reply
            var postReply = _postReplyService.GetById(id);

            //Get the application user that will write the reply for this post
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyEditModel
            {
                Id = postReply.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                Created = postReply.Created,
                ReplyContent = postReply.Content,

                EditedCreatedDate = DateTime.Now,
                VoteCount = postReply.VoteCount,
                IsEdited = postReply.IsEdited,

                PostId = postReply.Post.Id,
                ForumId = postReply.Post.Forum.Id,
                ForumImageUrl = postReply.Post.Forum.ImageUrl,
                ForumName = postReply.Post.Forum.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditContent(PostReplyEditModel model)
        {

            await _postReplyService.EditPostReplyContent(model.Id, model.ReplyContent);

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }


        //Delete
        public async Task<IActionResult> Delete (int id)
        {
            var postReply = _postReplyService.GetById(id);

            await _postReplyService.DeleteAllReplies(id);

            return NoContent();
        }

    }
}