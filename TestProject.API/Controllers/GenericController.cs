using Microsoft.AspNetCore.Mvc;
using MediatR;
using TestProject.Core.Commands;
using System.Threading.Tasks;

namespace TestProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenericController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetGenericQuery());
            return Ok(result);
        }
    }
}
