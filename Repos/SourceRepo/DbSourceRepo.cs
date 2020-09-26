using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public class DbSourceRepo : ISourceRepo
    {
        private readonly AppDbContext _dbContext;

        public DbSourceRepo(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<Source> CreateSource(string fields)
        {
            var source = new Source
            {
                Fields = fields,
                Annotations = new List<Annotation>()
            };

            await _dbContext.Sources.AddAsync(source);
            await _dbContext.SaveChangesAsync();

            return source;
        }

        public async Task<Source> DeleteSource(int id)
        {
            var source = await _dbContext.Sources.FindAsync(id);
            if(source != null)
            {
                _dbContext.Remove(source);
                await _dbContext.SaveChangesAsync();
            }

            return source;
        }

        public IEnumerable<Source> GetSources(IEnumerable<int> annotationIds)
        {
            return _dbContext.Sources
                .Where(source => source.Annotations
                    .Any(ann => annotationIds.Contains(ann.Id)));
        }
    }
}