using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Post;
using BibleForum.Models.Reply;
using BibleForum.Models.ReplySupportingBibleVerse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;

        //Made available from the Identity Membership System
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forumService, UserManager<ApplicationUser> userManager, IApplicationUser userService) {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            _userService = userService;
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
                Replies = replies,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                ForumImage = post.Forum.ImageUrl,
                IsAuthorAdmin = IsAuthorAdmin(post.User)
            };

            return View(model);
        }

        //A method to create a new Post FROM the Forum Index View
        [Authorize]
        public IActionResult Create (int id)
        {
            //Note ID is Forum ID
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                //User avaible from Claims Principle
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;

            var post = BuildPost(model, user);

            //Use "await" because of the "async" method
            await _postService.Add(post);

            //Implement User Rating Management here
            await _userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            // Explain why forum is being overloaded here <-
            var forum = _forumService.GetById(model.ForumId);

            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
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
                ReplyContent = reply.Content,
                PostReplySupportingBibleVerses = reply.PostReplySupportingBibleVerse
                    .Select(supportVerse => new PostReplySupportingBibleVerseModel
                        {
                            Id = supportVerse.Id,
                            BibleBook = supportVerse.BibleBook,
                            BibleChapter = supportVerse.BibleChapter,
                            BibleTranslation = supportVerse.BibleTranslation,
                            BibleVerse = supportVerse.BibleVerse,
                            Created = supportVerse.Created,
                        }),
                VoteCount = reply.VoteCount,
                EditedCreatedDate = reply.EditedDate,
                IsEdited = reply.IsEdited,

                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }
    }
}