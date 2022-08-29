using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IMediator mediator;

        public DirectorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateDirector")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
