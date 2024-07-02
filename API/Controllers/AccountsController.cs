using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterQueryResult>> Register(RegisterQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginQueryResult>> Login(LoginQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
