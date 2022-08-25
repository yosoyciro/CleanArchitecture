﻿using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IMediator mediator;

        public VideoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IEnumerable<VideoVm>> Get()
        //{

        //}

        [HttpGet("{username}", Name = "GetVideosByUsername")]
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
        {
            var query = new GetVideosListQuery(username);
            var videos = await mediator.Send(query);

            return Ok(videos);
        }
    }
}
