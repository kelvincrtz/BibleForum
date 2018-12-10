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

        public IActionResult Reply(int id)
        {
            return View();
        }
    }
}