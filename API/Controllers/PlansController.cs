﻿using API.Data.Dtos;
using API.Handlers.Plans.AddPlan;
using API.Handlers.Plans.AddRecord;
using API.Handlers.Plans.ArchivePlan;
using API.Handlers.Plans.CreatePlan;
using API.Handlers.Plans.GetPlan;
using API.Handlers.Plans.GetPlans;
using API.Handlers.Plans.GetPlanTemplates;
using API.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class PlansController(IMediator mediator) : BaseApiController(mediator)
    {

        [HttpPost("Create")]
        public async Task<ActionResult> CreatePlan(CreatePlanCommand query)
        {
            var reslut = await _mediator.Send(query);

            return Created(reslut.Id.ToString(), reslut);
        }

        [HttpGet("Templates")]
        public async Task<ActionResult<PagedResult<FitnessPlanTemplateDto>>> GetPlanTemplates([FromQuery] GetPlanTemplatesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("add/{id}")]
        public async Task<ActionResult> AddPlan(int id)
        {
            var query = new AddPlanCommand() { Id = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult> AddRecord(AddRecordsCommand query)
        {
            var reslut = await _mediator.Send(query);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetPlansResponse>> GetPlans()
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
            var query = new ArchivePlanCommand { PlanId = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
