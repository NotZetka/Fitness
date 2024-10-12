using API.Data.Dtos;
using API.Handlers.Accounts.List;
using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
using API.Handlers.Accounts.Register.Member;
using API.Handlers.Accounts.Register.Trainer;
using API.Utilities;
using API.Utilities.Static;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController(IMediator mediator, IMapper mapper) : BaseApiController(mediator)
    {

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterCommand query)
        {
            if(query.Role == RoleNames.Member)
            {
                var response = await _mediator.Send(mapper.Map<RegisterMemberCommand>(query));
                return Ok(response.Token);
            }
            else if(query.Role == RoleNames.Trainer)
            {
                var response = await _mediator.Send(mapper.Map<RegisterTrainerCommand>(query));
                return Ok(response.Token);
            }
            return BadRequest("Invalid role name");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response.Token);
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<ActionResult<PagedResult<UserDto>>> GetAccountList([FromQuery] GetAccountsListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
