using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibleForum.Models.ReplySupportingBibleVerse
{
    public class PostReplySupportingBibleVerseListingModel
    {
        public int Id { get; set; }
        public string BibleChapter { get; set; }
        public string BibleVerse { get; set; }
        public string BibleTranslation { get; set; }
        public DateTime Created { get; set; }
    }
}
