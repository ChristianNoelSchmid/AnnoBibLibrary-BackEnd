using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public class DbAnnotationRepo : IAnnotationRepo
    {
        private readonly AppDbContext _dbContext;

        public DbAnnotationRepo(AppDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<Annotation> CreateAnnotation(int sourceId)
        {
            var annotation = new Annotation
            {
                Notes = "",
                QuoteData = "[]",
                SourceId = sourceId,
            };

            await _dbContext.Annotations.AddAsync(annotation);
            await _dbContext.SaveChangesAsync();

            return annotation;
        }

        public async Task<Annotation> DeleteAnnotation(int id)
        {
            var annotation = await _dbContext.Annotations.FindAsync(id);
            if(annotation != null)
            {
                _dbContext.Annotations.Remove(annotation);
                await _dbContext.SaveChangesAsync();
            }

            return annotation;
        }

        public async Task<Annotation> GetAnnotation(int id)
        {
            var annotation = await _dbContext.Annotations.FindAsync(id);

            if(annotation == null)
                throw new AnnotationNotFoundException();

            return annotation;
        }

        public IEnumerable<Annotation> GetAnnotations(IEnumerable<int> ids)
        {
            return _dbContext.Annotations
                .Where(ann => ids.Contains(ann.Id));
        }

        public IEnumerable<Annotation> GetSourceAnnotations(int sourceId)
        {
            return _dbContext.Annotations
                .Where(ann => ann.SourceId == sourceId);
        }

        public async Task<Annotation> UpdateAnnotation(int id, string notes, string quoteData)
        {
            var annotation = await _dbContext.Annotations.FindAsync(id);

            if(annotation == null) 
                throw new AnnotationNotFoundException();
            
            annotation.Notes = notes;
            annotation.QuoteData = quoteData;

            await _dbContext.SaveChangesAsync();

            return annotation;
        }
    }
}