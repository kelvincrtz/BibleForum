﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.ReplySupportingBibleVerse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    [Authorize]
    public class SupportingVerseController : Controller
    {
        private readonly IPost _postService;
        private readonly IPostReply _postReplyService;
        private readonly IPostReplySupportingBibleVerse _supportingBibleVerseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public SupportingVerseController(IPost postService, IPostReply postReplyService, UserManager<ApplicationUser> userManager, IApplicationUser userService, IPostReplySupportingBibleVerse supportingBibleVerseService)
        {
            _postService = postService;
            _postReplyService = postReplyService;
            _supportingBibleVerseService = supportingBibleVerseService;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Create(int id)
        {
            //Get post and track for reply
            var postReply = _postReplyService.GetById(id);

            //Get the application user that will write the reply for this post
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplySupportingBibleVerseModel
            {
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                Created = DateTime.Now,

                PostId = postReply.Post.Id,
                PostTitle = postReply.Post.Title,
                PostReplyId = postReply.Id
            };

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var verse = _supportingBibleVerseService.GetById(id);

            await _supportingBibleVerseService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddSupportingVerse(PostReplySupportingBibleVerseModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var replySupportingVerse = BuildSupportingVerse(model, user);

            await _supportingBibleVerseService.Add(replySupportingVerse);

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        private PostReplySupportingBibleVerse BuildSupportingVerse(PostReplySupportingBibleVerseModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);
            var postReply = _postReplyService.GetById(model.PostReplyId);

            return new PostReplySupportingBibleVerse
            {
                BibleChapter = model.BibleChapter,
                BibleTranslation = model.BibleTranslation,
                BibleVerse = model.BibleVerse,
                BibleBook = model.BibleBook,
                Post = post,
                PostReply = postReply,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}