using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.Repos;
using AnnoBibLibrary.RouteModels;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Controllers
{
    [Route("libraries")]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly ILibraryRepo _libraryRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IAnnotationLinkRepo _annotationLinkRepo;

        public LibraryController(
            ILogger<LibraryController> logger,
            ILibraryRepo libraryRepo,
            IAnnotationLinkRepo annotationLinkRepo,
            UserManager<ApplicationUser> userManager
        ) {
            _logger = logger;
            _libraryRepo = libraryRepo;
            _annotationLinkRepo = annotationLinkRepo;
            _userManager = userManager;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(string title, string description, string keywordGroups)
        {
            var library = await _libraryRepo.CreateLibrary(title, description, keywordGroups);
            return Ok(new JsonResult(library));
        }

        [HttpGet, Route("get/byid")]
        public async Task<IActionResult> GetById(int libraryId)
        {
            try
            {
                var library = await _libraryRepo.GetLibrary(libraryId);
                return Ok(new JsonResult(library));
            }
            catch(LibraryNotFoundException) 
            {
                return BadRequest($"Library with id={libraryId} not found.");
            }
        }

        [HttpPut, Route("get/byuserid")]
        public IActionResult GetByUserId(string id)
        {
            return Ok(_libraryRepo.GetUserLibraries(id));
        }

        [HttpGet, Route("get/byannotationid")]
        public async Task<IActionResult> GetByAnnotationId(int annotationId)
        {
            var libraryIds = await _annotationLinkRepo.GetLibraryIds(annotationId);
            var libraries = _libraryRepo.GetLibraries(libraryIds);

            return Ok(JsonConvert.SerializeObject(libraries));
        }

        [HttpPut, Route("adduser")]
        public async Task<IActionResult> AddUser([FromBody]UserLibrary userLibrary)
        {
            await _libraryRepo.AddUser(userLibrary.LibraryId, userLibrary.UserId);

            return Ok();
        }
    }
}