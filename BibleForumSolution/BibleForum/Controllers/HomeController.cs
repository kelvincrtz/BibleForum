using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BibleForum.Models;
using BibleForum.Models.Home;
using BibleForum.Data;
using BibleForum.Models.Post;
using BibleForum.Data.Models;
using BibleForum.Models.Forum;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Extensions.Configuration;

namespace BibleForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;
        private readonly IConfiguration _configuration;

        public HomeController(IPost postService, IConfiguration configuration)
        {
            _postService = postService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            /*TEST ONLY for other Projects SUCCESS
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection con = new SqlConnection(connectionString);
            */

            var model = BuildHomeIndexModel();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLastestPosts(10);

            var posts = latestPosts.Select(post => new PostListingModel
            {
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                Id = post.Id,
                RepliesCount = post.Replies.Count(),
                Title = post.Title,
                //Get the Corresponding Forum for this Post via Post.ForumID
                Forum = GetForumListingModelForPost(post)
            });

            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel GetForumListingModelForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl,
                Title = forum.Title
            };
        }
    }
}
