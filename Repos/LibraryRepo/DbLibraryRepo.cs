using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public class DbLibraryRepo : ILibraryRepo
    {
        private readonly AppDbContext _dbContext;

        public DbLibraryRepo(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<Library> CreateLibrary(string title, string description, string keywordGroups)
        {
            var library = new Library
            {
                Title = title,
                Description = description,
                KeywordGroups = keywordGroups,
            };

            _dbContext.Libraries.Add(library);
            await _dbContext.SaveChangesAsync();

            return library;
        }

        public async Task<Library> GetLibrary(int id)
        {
            var library = await _dbContext.Libraries.FindAsync(id);

            if(library == null)
                throw new LibraryNotFoundException();

            return library;
        }

        public IEnumerable<Library> GetLibraries(IEnumerable<int> ids)
        {
            return _dbContext.Libraries
                .Where(library => ids.Contains(library.Id));
        }
    }
}