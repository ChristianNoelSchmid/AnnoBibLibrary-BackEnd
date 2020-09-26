using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public class InMemoryLibraryRepo : ILibraryRepo
    {
        private List<Library> _libraries;

        public InMemoryLibraryRepo()
        {
            _libraries = new List<Library>();
        }

        public Task<Library> CreateLibrary(string title, string description, string keywordGroups)
        {
            var library = new Library
            {
                Id = _libraries.Count,
                Title = title,
                Description = description,
                KeywordGroups = keywordGroups,
            };

            _libraries.Add(library);
            return Task.Run(() => library);
        }

        public IEnumerable<Library> GetLibraries(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<Library> GetLibrary(int id)
        {
            return Task.Run(
                () => _libraries.FirstOrDefault(lib => lib.Id == id)
            );
        }
    }
}