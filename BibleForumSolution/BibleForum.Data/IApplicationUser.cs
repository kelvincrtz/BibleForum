using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        //Task for async upload method - Uri for Url Azure Blob
        Task SetProfileImage(string id, Uri uri);

        //What is Type?
        Task UpdateUserRating(string userId, Type type);

    }
}
