using InternSotatek.Personal.Application.Usecases.Account.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> Login([FromBody] LoginUserCommand query)
		{
			var response = await _mediator.Send(query);
			return Ok(response);
		}
	}
}
