﻿using API.Handlers.Messages.GetMessageThread;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController(IMediator mediator) : BaseApiController(mediator)
    {

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetMessageThread(int userId)
        {
            var query = new GetMessageThreadQuery() { userId = userId };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
