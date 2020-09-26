using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    /// <summary>
    /// The in-memory repository of Sources. Used for
    /// testing and development
    /// </summary>
    public class InMemorySourceRepo : ISourceRepo
    {
        private List<Source> _sources;
        
        public InMemorySourceRepo()
        {
            _sources = new List<Source>();
        }

        public Task<Source> CreateSource(string fields)
        {
            var source = new Source
            {
                Id = _sources.Count,
                Fields = fields,
                Annotations = new List<Annotation>() 
            };

            _sources.Add(source);

            return Task.Run(() => source);
        }

        public async Task<Source> DeleteSource(int id)
        {
            var source = _sources.Find(source => source.Id == id);
            if(source != null) _sources.Remove(source);

            return await Task.Run(() => source);
        }

        public IEnumerable<Source> GetSources(IEnumerable<int> annotationIds)
        {
            return annotationIds.Select(
                id => _sources.Find(
                    source => source.Annotations.Any(ann => ann.Id == id)
                )
            );
        }
    }
}