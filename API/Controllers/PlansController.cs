using API.Handlers.Plans.AddPlan;
using API.Handlers.Plans.AddRecord;
using API.Handlers.Plans.ArchivePlan;
using API.Handlers.Plans.GetPlan;
using API.Handlers.Plans.GetPlans;
using API.Handlers.Plans.GetPlanTemplates;
using API.Handlers.Plans.Publish;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class PlansController : BaseApiController
    {
        public PlansController(IMediator mediator) : base(mediator) { }


        [HttpPost("Publish")]
        public async Task<ActionResult> PublicPlan(PublishPlanQuery query)
        {
            var reslut = await _mediator.Send(query);

            return Created(reslut.Id.ToString(), reslut);
        }

        [HttpGet("Templates")]
        public async Task<ActionResult> GetPlanTemplates()
        {
            var query = new GetPlanTemplatesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("add/{id}")]
        public async Task<ActionResult> AddPlan(int id)
        {
            var query = new AddPlanQuery() { Id = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult> AddRecord(AddRecordsQuery query)
        {
            var reslut = await _mediator.Send(query);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetPlans()
        {
            var query = new GetPlansQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlan(int id)
        {
            var query = new GetPlanQuery { PlanId = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPatch("archive/{id}")]
        public async Task<ActionResult> ArchivePlan(int id)
        {
            var query = new ArchivePlanQuery { PlanId = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPatch("visibility/{id}")]
        public async Task<ActionResult> ChangeVisibilty(int id)
        {
            var query = new ArchivePlanQuery { PlanId = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
