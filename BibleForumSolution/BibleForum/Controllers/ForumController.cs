using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Models.Forum;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
            });

            var model = new ForumIndexModel {
                ForumListing = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            
            return View();
        }
    }
}