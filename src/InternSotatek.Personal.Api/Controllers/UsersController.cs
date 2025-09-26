using FluentValidation;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Delete;
using InternSotatek.Personal.Application.Usecases.Users.Commands.SoftDelete;
using InternSotatek.Personal.Application.Usecases.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Create;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Update;
using InternSotatek.Personal.Application.Usecases.Users.Queries.GetUsersList;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Create new user")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
		{
			var reponse = await _mediator.Send(command);
			return Ok(reponse);
		}

		[HttpGet]
        [SwaggerOperation(Summary = "Get all users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query)
		{
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}

		[HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by id")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
		{
			var query = new GetUserByIdQuery { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}

		[HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, [FromRoute] Guid id)
		{
			command.Id = id;
			var reponse = await _mediator.Send(command);
			return Ok(reponse);
		}

		[HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete user")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
		{
			var query = new DeleteUserCommand { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}

		[HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Soft delete user")]

        public async Task<IActionResult> SoftDeleteUser([FromRoute] Guid id)
		{
			var query = new SoftDeleteUserCommand { Id = id };
			var reponse = await _mediator.Send(query);
			return Ok(reponse);
		}
	}
}
