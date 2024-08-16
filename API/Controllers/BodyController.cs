using API.Handlers.BodyWeight.AddBodyWeightRecord;
using API.Handlers.BodyWeight.GetBodyWeight;
using API.Handlers.BodyWeight.SetHeight;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class BodyController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPut]
        public async Task<ActionResult> SetHeight(SetHeightCommand query)
        {
            await _mediator.Send(query);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddBodyweightRecord(AddBodyWeightRecordCommand query)
        {
            await _mediator.Send(query);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetBodyWeightResponse>> GetBodyWeight()
        {
            var query = new GetBodyWeightQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}
