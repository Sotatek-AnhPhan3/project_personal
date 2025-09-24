using FluentValidation;
using InternSotatek.Personal.Application.Users.UseCases.Commands.Create;
using InternSotatek.Personal.Application.Users.UseCases.Commands.Delete;
using InternSotatek.Personal.Application.Users.UseCases.Commands.SoftDelete;
using InternSotatek.Personal.Application.Users.UseCases.Commands.Update;
using InternSotatek.Personal.Application.Users.UseCases.Queries.GetUserById;
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById([FromRoute] Guid id)
		{
			var query = new GetUserByIdQuery { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, [FromRoute] Guid id)
		{
			command.Id = id;
			var reponse = await _mediator.Send(command);
			return Ok(reponse);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
		{
			var query = new DeleteUserCommand { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> SortDeleteUser([FromRoute] Guid id)
		{
			var query = new SoftDeleteUserCommand { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}
	}
}
