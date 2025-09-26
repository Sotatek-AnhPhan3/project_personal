using InternSotatek.Personal.Application.Common.Dtos;
using InternSotatek.Personal.Application.Usecases.Account.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InternSotatek.Personal.Api.Controllers
{
    [ApiController]
	[Route("api/v1/[controller]")]
	public class AccountController : ControllerBase
	{
		private IMediator _mediator;
		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
        [SwaggerOperation(Summary = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand query)
		{
			var response = await _mediator.Send(query);
			return Ok(ApiResponse<LoginUserResponse>.Success(response));
		}
	}
}
