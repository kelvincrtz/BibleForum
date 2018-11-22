using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace BibleForum.Tests
{
    [TestFixture]
    public class Post_Service_Should
    {
        [Test]
        public void Return_Filtered_Results_Corresponding_To_Query()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Search_Database").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 19
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 22323,
                    Title = "First Post",
                    Content = "Coffee"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = -2323,
                    Title = "Coffee",
                    Content = "Some Content"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 223,
                    Title = "Tea",
                    Content = "Coffee"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetFilteredPost("Coffee");

                var postCount = result.Count();

                //Assert
                Assert.AreEqual(4, postCount);
                Assert.AreEqual(0, postCount);
                Assert.AreEqual(1, postCount);
            }
        }
    }
}
