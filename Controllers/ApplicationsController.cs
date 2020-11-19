using BearTracApi.Models;
using BearTracApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BearTracApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationService _ApplicationService;

        public ApplicationsController(ApplicationService applicationService)
        {
            _ApplicationService = applicationService;
        }

        [HttpGet]
        public ActionResult<List<Application>> Get() =>
            _ApplicationService.Get();

        [HttpGet("{id:length(24)}", Name = "GetApplication")]
        public ActionResult<Application> Get(string id)
        {
            var application = _ApplicationService.Get(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        [HttpPost]
        public ActionResult<Application> Create(Application application)
        {
            _ApplicationService.Create(application);
            return CreatedAtRoute("GetApplication", new { id = application.Id.ToString() }, application);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Application applicationIn)
        {
            var application = _ApplicationService.Get(id);

            if (application == null)
            {
                return NotFound();
            }

            _ApplicationService.Update(id, applicationIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var application = _ApplicationService.Get(id);

            if (application == null)
            {
                return NotFound();
            }

            _ApplicationService.Remove(application.Id);

            return NoContent();
        }
    }
}