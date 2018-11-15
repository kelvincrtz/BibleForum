using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BibleForum.Data;
using BibleForum.Data.Models;
using BibleForum.Models.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BibleForum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                Email = user.Email,
                MemberSince = user.MemberSince,
                ProfileImageUrl = user.ImageUrl,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            //Connect to an Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");
           
            //Get Blob Storage
            var container = _uploadService.GetCloudBlobContainer(connectionString, "profile-images");

            //Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            // On that block blob, Upload our file < --- fie Uploaded to the cloud
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            //Set the User's profile image to the URL
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            //Redirect to the user's profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });

        } 
    }
}