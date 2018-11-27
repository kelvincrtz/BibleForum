using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Data
{
    public interface IPostReplySupportingBibleVerse
    {
        PostReplySupportingBibleVerse GetById(int id);
        IEnumerable<PostReplySupportingBibleVerse> GetAll();

        Task Add(int id);
        Task Delete(int id);
        Task EditPostReplyVerseContent(int id, string newContent, string newChapter, string newTranslation);
    }
}
