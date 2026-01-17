using Microsoft.AspNetCore.Mvc;
using PulseCheck.Core.DTO;
using PulseCheck.Core.Interfaces;
using PulseCheck.Core.Models;

namespace PulseCheck.API.Controllers
{
    [ApiController]
    public class APIEndpointController : ControllerBase
    {
        private IAPIEndpointRepository _endpointRepository;

        public APIEndpointController(IAPIEndpointRepository endpointRepository)
        {
            this._endpointRepository = endpointRepository;
        }

        [HttpPost("api/Register")]
        public async Task<ActionResult<APIEndpoint>> RegisterAPIEndpoint(RegisterAPIEndpointDTO endpointDTO)
        {
            if (endpointDTO == null)
            {
                return BadRequest("Request cannot be empty");
            }
            try
            {
                var result = await _endpointRepository.RegisterEndpoint(endpointDTO);
                return CreatedAtAction("RegisterAPIEndpoint", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("api/URLs")]
        public async Task<ActionResult<List<APIEndpoint>>> GetRegisteredURLs()
        {
            try
            {
                var result = await _endpointRepository.GetRegisteredEndpoints();
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("api/URLs/{registerer}")]
        public async Task<ActionResult<List<APIEndpoint>>> GetRegisteredEndpointsByRegisterer(string registerer)
        {
            try
            {
                var result = await _endpointRepository.GetRegisteredEndpointsByRegisterer(registerer);
                if(result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
