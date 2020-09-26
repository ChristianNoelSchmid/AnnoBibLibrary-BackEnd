using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public interface ISourceRepo
    {
        /// <summary>
        /// Creates a new Source
        /// </summary>
        Task<Source> CreateSource(string fields);

        /// <summary>
        /// Deletes a Source, by its Id
        /// </summary>
        Task<Source> DeleteSource(int id);

        /// <summary>
        /// Retrieves a collection of Sources based on
        /// the supplied Annotations (1 Source per Annotation)
        /// </summary>
        IEnumerable<Source> GetSources(IEnumerable<int> annotationIds);
    }
}