using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Controllers
{
    [Route("annotations")]
    public class AnnotationController : ControllerBase
    {
        private readonly ILogger<AnnotationController> _logger;
        private readonly IAnnotationRepo _annotationRepo;
        private readonly IAnnotationLinkRepo _annotationLinkRepo;

        public AnnotationController(
            ILogger<AnnotationController> logger,
            IAnnotationRepo annotationRepo,
            IAnnotationLinkRepo annotationLinkRepo
        ) {
            _logger = logger;
            _annotationRepo = annotationRepo;
            _annotationLinkRepo = annotationLinkRepo;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(int libraryId, int sourceId)
        {
            var annotation = await this._annotationRepo.CreateAnnotation(sourceId);
            return Ok(new JsonResult(annotation));
        }

        [HttpGet, Route("get/byid")]
        public async Task<IActionResult> GetById(int annotationId)
        {
            try
            {
                return Ok(
                    JsonConvert.SerializeObject(
                        await _annotationRepo.GetAnnotation(annotationId)
                    )
                );
            }
            catch(AnnotationNotFoundException)
            {
                return BadRequest($"Annotation with id={annotationId} not found.");
            }
        }

        [HttpGet, Route("get/bylibraryid")]
        public async Task<IActionResult> GetByLibraryId(int libraryId)
        {
            var annotationIds = await _annotationLinkRepo.GetAnnotationIds(libraryId);
            var annotations = new List<Annotation>(_annotationRepo.GetAnnotations(annotationIds));

            return Ok(JsonConvert.SerializeObject(annotations));
        }
    
        [HttpPut, Route("update")]
        public async Task<IActionResult> Update(int annotationId, string notes, string quoteData)
        {
            try
            {
                var annotation = await _annotationRepo.UpdateAnnotation(annotationId, notes, quoteData);
                return Ok(JsonConvert.SerializeObject(annotation));
            }
            catch(AnnotationNotFoundException)
            {
                return BadRequest($"Annotation with id={annotationId} not found.");
            }
        }
    }
}