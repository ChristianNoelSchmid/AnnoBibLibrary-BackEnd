using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public class InMemoryAnnotationRepo : IAnnotationRepo
    {
        private List<Annotation> _annotations;

        public InMemoryAnnotationRepo()
        {
            _annotations = new List<Annotation>();
        }

        public Task<Annotation> CreateAnnotation(int sourceId)
        {
            int id = _annotations.Count;

            var annotation = new Annotation 
            {
                Id = id,
                Notes = "",
                QuoteData = "[]",
                SourceId = sourceId,
            };

            this._annotations.Add(annotation);
            return Task.Run(() => annotation);
        }

        public IEnumerable<Annotation> GetSourceAnnotations(int sourceId)
        {
            return this._annotations
                .Where(ann => ann.SourceId == sourceId);
        }

        public Task<Annotation> DeleteAnnotation(int id)
        {
            var annotation = _annotations.Find(ann => ann.Id == id);
            if(annotation != null) _annotations.Remove(annotation);

            return Task.Run(() => annotation);
        }

        public Task<Annotation> UpdateAnnotation(int id, string notes, string quoteData)
        {
            var annotation = _annotations.Find(ann => ann.Id == id);
            if(annotation != null) 
            {
                annotation.Notes = notes;
                annotation.QuoteData = quoteData;
            }

            return Task.Run(() => annotation);
        }

        public IEnumerable<Annotation> GetAnnotations(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<Annotation> GetAnnotation(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}