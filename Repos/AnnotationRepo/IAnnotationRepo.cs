using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;

namespace AnnoBibLibrary.Repos
{
    public interface IAnnotationRepo
    {
        Task<Annotation> CreateAnnotation(int sourceId);
        Task<Annotation> GetAnnotation(int id);
        IEnumerable<Annotation> GetAnnotations(IEnumerable<int> ids);
        IEnumerable<Annotation> GetSourceAnnotations(int sourceId);
        Task<Annotation> DeleteAnnotation(int id);
        Task<Annotation> UpdateAnnotation(int id, string notes, string quoteData);
    }
}