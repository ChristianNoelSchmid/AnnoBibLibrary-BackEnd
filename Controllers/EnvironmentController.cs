using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AnnoBibLibrary.Controllers
{
    [Route("")]
    public class EnvironmentController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public EnvironmentController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet("routes", Name = "ApiEnvironmentGetAllRoutes")]
        public IActionResult GetAllRoutes()
        {

            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(
                ad => ad.AttributeRouteInfo != null).Select(ad => new
                {
                    Name = ad.AttributeRouteInfo.Name,
                    Template = ad.AttributeRouteInfo.Template
                }).ToList();
            return Ok(routes);
        }
    }
}