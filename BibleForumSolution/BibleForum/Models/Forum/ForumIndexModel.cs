using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumListingOld { get; set; }
        public IEnumerable<ForumListingModel> ForumListingNew { get; set; }
    }
}
