using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Repos;
using AnnoBibLibrary.RouteModels;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Controllers
{
    [Route("sources")]
    public class SourceController : ControllerBase
    {
        private readonly ILogger<SourceController> _logger;
        private readonly ISourceRepo _sourceRepo;

        private readonly ILibraryRepo _libraryRepo;
        private readonly IAnnotationRepo _annotationRepo;
        private readonly IAnnotationLinkRepo _annotationLinkRepo;

        public SourceController(
            ILogger<SourceController> logger, 
            ISourceRepo sourceRepo,
            ILibraryRepo libraryRepo,
            IAnnotationRepo annotationRepo,
            IAnnotationLinkRepo annotationLinkRepo
        ) {
            _logger = logger;
            _sourceRepo = sourceRepo; 
            _libraryRepo = libraryRepo;
            _annotationRepo = annotationRepo;
            _annotationLinkRepo = annotationLinkRepo;
        }

        /// <summary>
        /// Creating a new Source requires three separate db transactions.
        /// First, the Library it's associated with must be retrieved.
        /// Second, the Source is created.
        /// Third, a new Annotation is created, linking the Source to the Library
        /// </summary>
        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(int libraryId, string fields)
        {
            // Ensure that the Library being added to exists.
            // If not, return a BadRequest code
            try 
            {
                await this._libraryRepo.GetLibrary(libraryId);
            }
            catch(LibraryNotFoundException)
            {
                return BadRequest($"Library with id={libraryId} not found.");
            }

            // Create the Source
            var source = await this._sourceRepo.CreateSource(fields);

            // Create the Annotation, and add it to the source's list
            var annotation = await this._annotationRepo.CreateAnnotation(source.Id);

            // Create the AnnotationLink
            await this._annotationLinkRepo.CreateAnnotationLink(libraryId, annotation.Id);

            this._logger.LogInformation(
                $"Created new Source (id={source.Id}) and Annotation (id={annotation.Id}), associated with Library (id=${libraryId})."
            );

            return Ok(JsonConvert.SerializeObject(source));
        }

        /// <summary>
        /// Retrieves all Sources associated with the specified library,
        /// with the options of including each Source's associated Annotations
        /// </summary>
        [Authorize]
        [HttpPut, Route("get/bylibraryid")]
        public async Task<IActionResult> GetSources([FromBody]LibrarySources librarySources)
        {
            try
            {
                var annotationIds = await _annotationLinkRepo.GetAnnotationIds(librarySources.LibraryId);

                if(librarySources.IncludeAnnotations)
                    // Load the annotations in question into memory, so that
                    // upon Sources serialization they're included
                    foreach(var annotation in _annotationRepo.GetAnnotations(annotationIds));

                var sources = _sourceRepo.GetSources(annotationIds);

                return Ok(JsonConvert.SerializeObject(sources));
            }
            catch(LibraryNotFoundException)
            {
                return BadRequest($"Library with id={librarySources.LibraryId} not found.");
            }
        }
    }
}