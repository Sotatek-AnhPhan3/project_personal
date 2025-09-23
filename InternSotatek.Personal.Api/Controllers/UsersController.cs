using FluentValidation;
using InternSotatek.Personal.Application.Users.UseCases.Commands.Create;
using InternSotatek.Personal.Application.Users.UseCases.Queries.GetUsersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternSotatek.Personal.Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;
		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
		{
			var reponse = await _mediator.Send(command);
			return Ok(reponse);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query)
		{
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}
	}
}
