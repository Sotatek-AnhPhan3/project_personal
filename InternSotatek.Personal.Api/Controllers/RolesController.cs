using InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSotatek.Personal.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public RolesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}
	}
}
