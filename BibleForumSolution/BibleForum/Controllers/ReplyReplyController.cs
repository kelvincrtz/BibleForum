﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.ReplyReply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    [Authorize]
    public class ReplyReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly IPostReply _postReplyService;
        private readonly IPostReplyReply _postReplyReplyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public ReplyReplyController(IPost postService, IPostReply postReplyService, IPostReplyReply postReplyReplyService,
            UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postService = postService;
            _postReplyService = postReplyService;
            _postReplyReplyService = postReplyReplyService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Create(int id)
        {
            //Get postReplyID and track for reply
            var postReply = _postReplyService.GetById(id);

            //Get postID and track for reply
            var post = _postService.GetById(postReply.Post.Id);

            //Get the application user that will write the reply for this post
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyReplyModel
            {
                PostId = post.Id,
                PostReplyId = postReply.Id,
                PostReplyContent = postReply.Content,
                ReplyOriginalAuthor = postReply.User.UserName,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                Created = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await _postReplyReplyService.Add(reply);

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var postReplyReply = _postReplyReplyService.GetById(id);

            await _postReplyReplyService.Delete(id);

            return NoContent();
        }

        public async Task<IActionResult> Edit(int id)
        {
            //Get post and track for reply
            var postReplyReply = _postReplyReplyService.GetById(id);

            ViewBag.JumpToDivId = postReplyReply.PostReply.Id;

            //Get the application user that will write the reply for this post
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyReplyEditModel
            {
                Id = postReplyReply.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                ReplyContent = postReplyReply.Content,

                EditedCreatedDate = DateTime.Now,
                VoteCount = postReplyReply.VoteCount,
                IsEdited = postReplyReply.IsEdited,

                PostId = postReplyReply.Post.Id,
                PostReplyId = postReplyReply.PostReply.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditContent(PostReplyReplyEditModel model)
        {

            await _postReplyReplyService.EditPostReplyContent(model.Id, model.ReplyContent);

            //ViewBag.JumpToDivId = model.PostReplyId;

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }


        private PostReplyReply BuildReply(PostReplyReplyModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);
            var postReply = _postReplyService.GetById(model.PostReplyId);

            return new PostReplyReply
            {
                Post = post,
                PostReply = postReply,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}