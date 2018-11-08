using BibleForum.Data;
using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private ApplicationDbContext _dbContext;

        public ApplicationUserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _dbContext.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            //Get user first
            var user = GetById(id);

            //Pass profileImageUrl to uri.AbsoluteURi
            user.ImageUrl = uri.AbsoluteUri;

            //dbContext Update User
            _dbContext.Update(user);

            //await - Await because method is async Task, then Save Changes
            await _dbContext.SaveChangesAsync();

        }
    }
}
