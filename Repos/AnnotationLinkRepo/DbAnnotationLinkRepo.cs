using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnoBibLibrary.Repos
{
    public class DbAnnotationLinkRepo : IAnnotationLinkRepo
    {
        private readonly AppDbContext _dbContext;

        public DbAnnotationLinkRepo(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<AnnotationLink> CreateAnnotationLink(int libraryId, int annotationId)
        {
            var link = await _dbContext.AnnotationLinks
                .FirstOrDefaultAsync(link => 
                    link.LibraryId == libraryId && link.AnnotationId == annotationId
                );

            if(link != null)
                throw new AnnotationLinkAlreadyExistsException();

            link = new AnnotationLink
            {
                LibraryId = libraryId,
                AnnotationId = annotationId,
                KeywordValues = "" 
            };

            await _dbContext.AnnotationLinks.AddAsync(link);
            await _dbContext.SaveChangesAsync();

            return link;
        }

        public async Task<AnnotationLink> GetAnnotationLink(int libraryId, int annotationId)
        {
            var link = await _dbContext.AnnotationLinks
                .FirstOrDefaultAsync(link => 
                    link.LibraryId == libraryId && link.AnnotationId == annotationId
                );

            if(link == null)
                throw new AnnotationLinkNotFoundException();

            return link;
        }

        public async Task<AnnotationLink> SetLinkKeywords(int linkId, string keywords)
        {
            var link = await _dbContext.AnnotationLinks.FindAsync(linkId);

            if(link == null)
                throw new AnnotationLinkNotFoundException();

            link.KeywordValues = keywords;
            await _dbContext.SaveChangesAsync();

            return link;
        }

        public async Task<IEnumerable<int>> GetLibraryIds(int annotationId)
        {
            if(await _dbContext.Annotations.FindAsync(annotationId) == null)
                throw new AnnotationNotFoundException();

            return _dbContext.AnnotationLinks
                .Where(link => link.AnnotationId == annotationId)
                .Select(link => link.LibraryId);
        }

        public async Task<IEnumerable<int>> GetAnnotationIds(int libraryId)
        {
            if(await _dbContext.Libraries.FindAsync(libraryId) == null)
                throw new LibraryNotFoundException();

            return _dbContext.AnnotationLinks
                .Where(link => link.LibraryId == libraryId)
                .Select(link => link.AnnotationId);
        }
    }
}