using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Forum;
using BibleForum.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BibleForum.Controllers
{

    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ForumController(IForum forumService, IPost postService, IUpload uploadService, IConfiguration configuration)
        {
            _forumService = forumService;
            _postService = postService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => new ForumListingModel {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                NumberOfPosts = forum.Posts?.Count() ?? 0, //check to see if its null < ? >
                NumberOfUsers = _forumService.GetActiveUsers(forum.Id).Count(),
                ImageUrl = forum.ImageUrl,
                HasRecentPost = _forumService.HasRecentPost(forum.Id)
            });

            var model = new ForumIndexModel {
                ForumListing = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);

            //New List of empty List Post
            var posts = new List<Post>();

            posts = _postService.GetFilteredPost(forum, searchQuery).ToList();

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(int id, string SearchQuery)
        {
            //Reuse the Topic View - No need to create a new model or a new view
            return RedirectToAction("Topic", new { id, SearchQuery });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //Create a empty model and pass it to the view

            var model = new AddForumModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {
            var imageUri = "/images/users/default.png";

            if(model.ImageUpload != null)
            {
                var blockBlob = UploadForumImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            var forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUri
            };

            await _forumService.Create(forum);
            return RedirectToAction("Index", "Forum");
        }

        private CloudBlockBlob UploadForumImage(IFormFile file)
        {

            //Connect to an Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //Get Blob Storage
            var container = _uploadService.GetCloudBlobContainer(connectionString, "forum-images");

            //Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            // On that block blob, Upload our file < --- fie Uploaded to the cloud
            blockBlob.UploadFromStreamAsync(file.OpenReadStream()).Wait();

            return blockBlob;
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        //Overload method for Forum Type
        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}