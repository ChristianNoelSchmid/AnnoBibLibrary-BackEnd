using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public interface IAnnotationLinkRepo
    {
        Task<AnnotationLink> CreateAnnotationLink(int libraryId, int annotationId);
        Task<AnnotationLink> GetAnnotationLink(int libraryId, int annotationId);

        Task<AnnotationLink> SetLinkKeywords(int linkId, string keywords);

        Task<IEnumerable<int>> GetLibraryIds(int annotationId);

        Task<IEnumerable<int>> GetAnnotationIds(int libraryId);
    }
}