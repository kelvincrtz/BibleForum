using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Forum;
using BibleForum.Models.Post;
using BibleForum.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    public class SearchController : Controller
    {
        private IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFilteredPost(searchQuery);
            var areNoResults = (!String.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Title = post.Title,
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new SearchResultModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResults = areNoResults
            };

            return View(model);
        }

        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Description = forum.Description,
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Title = forum.Title
            };
        }
    }
}