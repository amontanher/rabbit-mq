using MessageBroker.Publisher.Model;
using MessageBroker.Publisher.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MessageBroker.Publisher.Controllers
{
    [ApiController]
    [Route("v1/messages")]
    public class PublisherController : ControllerBase
    {
        readonly PublisherService _service;
        
        public PublisherController(PublisherService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IActionResult InitiApp()
        {
            return Ok();
        }

        [HttpPost]
        [Route("born")]
        public IActionResult PublishMessage([FromBody] Born born)
        {
            try
            {
                _service.SendMessage(born);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }            
        }
    }
}