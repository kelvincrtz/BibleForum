using BibleForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public DataSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_dbContext);
            var userStore = new UserStore<ApplicationUser>(_dbContext);

            var user = new ApplicationUser
            {
                UserName = "BibleForumAdmin",
                NormalizedUserName = "bibleforumadmin",
                Email = "bibleforumadmin@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user, "adminpasswordnz");

            user.PasswordHash = hashedPassword;

            var hasAdminRole = _dbContext.Roles.Any(roles => roles.Name == "Admin");

            if (!hasAdminRole)
            {
                 roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                });
            }

            var hasSuperUser = _dbContext.Users.Any(u => u.NormalizedUserName == user.UserName);

            if (!hasSuperUser)
            {
                 userStore.CreateAsync(user);
                 userStore.AddToRoleAsync(user, "Admin"); //Add Admin role to the user
            }

            _dbContext.SaveChangesAsync();

            return Task.CompletedTask;

        }
    }
}
