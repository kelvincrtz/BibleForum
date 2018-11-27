using BibleForum.Data;
using BibleForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibleForum.Service
{
    public class PostReplySupportingBibleVerseService : IPostReplySupportingBibleVerse
    {
        private readonly ApplicationDbContext _dbContext;

        public PostReplySupportingBibleVerseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(PostReplySupportingBibleVerse postReplySupportingBibleVerse)
        {
            _dbContext.Add(postReplySupportingBibleVerse);
            await _dbContext.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostReplyVerseContent(int id, string newContent, string newChapter, string newTranslation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostReplySupportingBibleVerse> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostReplySupportingBibleVerse GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
