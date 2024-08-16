using API.Handlers.Accounts.List;
using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController(IMediator mediator) : BaseApiController(mediator)
    {

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterCommand query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<ActionResult<GetAccountsListResponse>> GetAccountList()
        {
            var query = new GetAccountsListQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
