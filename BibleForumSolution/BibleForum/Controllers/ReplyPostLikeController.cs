using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    public class ReplyPostLikeController : Controller
    {
        private readonly IPost _postService;
        private readonly IPostReply _postReplyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IPostReplyLike _postReplyLikeService;

        public ReplyPostLikeController(IPost postService, IPostReply postReplyService, UserManager<ApplicationUser> userManager, IApplicationUser userService, IPostReplyLike postReplyLikeService)
        {
            _postService = postService;
            _postReplyService = postReplyService;
            _userManager = userManager;
            _userService = userService;
            _postReplyLikeService = postReplyLikeService;
        }

        //Voting
        public async Task<IActionResult> Vote(int id)
        {
            var postReply = _postReplyService.GetById(id);

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var vote = BuildVote(postReply.Id, user);

            await _postReplyLikeService.Add(vote);

            return RedirectToAction("Index", "Post", new { id = postReply.Post.Id });

        }

        private PostReplyLike BuildVote(int postReplyId, ApplicationUser user)
        {
            var postReply = _postReplyService.GetById(postReplyId);

            return new PostReplyLike
            {
                PostReply = postReply,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}