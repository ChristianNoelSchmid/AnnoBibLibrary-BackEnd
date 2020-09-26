using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public interface ILibraryRepo
    {
        Task<Library> CreateLibrary(string title, string description, string keywordGroups);

        Task<Library> GetLibrary(int id);

        IEnumerable<Library> GetLibraries(IEnumerable<int> ids);
    }
}