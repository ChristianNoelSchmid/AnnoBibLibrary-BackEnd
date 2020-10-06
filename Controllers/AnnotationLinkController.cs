using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Controllers
{
    [Route("links")]
    public class AnnotationLinkController : ControllerBase
    {
        private readonly ILogger<AnnotationLinkController> _logger;
        private readonly IAnnotationLinkRepo _annotationLinkRepo;
        private readonly ILibraryRepo _libraryRepo;
        private readonly IAnnotationRepo _annotationRepo;

        public AnnotationLinkController(
            ILogger<AnnotationLinkController> logger,
            IAnnotationLinkRepo annotationLinkRepo,
            ILibraryRepo libraryRepo,
            IAnnotationRepo annotationRepo
        ) {
            _logger = logger;
            _annotationLinkRepo = annotationLinkRepo;
            _libraryRepo = libraryRepo;
            _annotationRepo = annotationRepo;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(int libraryId, int annotationId)
        {
            try
            {
                await _annotationRepo.GetAnnotation(annotationId);
                await _libraryRepo.GetLibrary(libraryId);

                var link = await _annotationLinkRepo.CreateAnnotationLink(libraryId, annotationId); 
                return Ok(new JsonResult(link));
            }
            catch(LibraryNotFoundException)
            {
                return BadRequest($"Library with id={libraryId} not found");
            }
            catch(AnnotationNotFoundException)
            {
                return BadRequest($"Annotation with id={annotationId} not found");
            }
            catch(AnnotationLinkAlreadyExistsException)
            {
                return BadRequest($"AnnotationLink with libraryId={libraryId} and annotationId={annotationId} already exists");
            }
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get(int libraryId, int annotationId)
        {
            try
            {
                var link =  await _annotationLinkRepo.GetAnnotationLink(libraryId, annotationId);
                return Ok(new JsonResult(link));
            } 
            catch(AnnotationLinkNotFoundException)
            {
                return BadRequest($"AnnotationLink with libraryId={libraryId} and annotationId={annotationId} not found.");
            }
        }

        [HttpPut, Route("setkeywords")]
        public async Task<IActionResult> SetKeywords(int linkId, string keywords)
        {
            try
            {
                var link = await _annotationLinkRepo.SetLinkKeywords(linkId, keywords);
                return Ok(JsonConvert.SerializeObject(link));
            }
            catch(AnnotationLinkNotFoundException)
            {
                return BadRequest($"AnnotationLink with id={linkId} not found.");
            }
        }
    }
}