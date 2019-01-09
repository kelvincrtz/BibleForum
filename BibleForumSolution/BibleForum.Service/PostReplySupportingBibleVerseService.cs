using BibleForum.Data;
using BibleForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Delete(int id)
        {
            var supportVerse = GetById(id);

            _dbContext.Remove(supportVerse);

            await _dbContext.SaveChangesAsync();
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
            return _dbContext.PostReplySupportingBibleVerses.Where(supportVerse => supportVerse.Id == id)
                .Include(supportVerse => supportVerse.PostReply)
                .Include(supportVerse => supportVerse.Post)
                .First();
        }
    }
}
