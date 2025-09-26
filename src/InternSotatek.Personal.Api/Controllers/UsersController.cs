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
using InternSotatek.Personal.Application.Common.Dtos;
using InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;
using InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;

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
			var response = await _mediator.Send(command);
			return Ok(ApiResponse<CreateUserResponse>.Success(response));
		}

		[HttpGet]
        [SwaggerOperation(Summary = "Get all users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query)
		{
			var response = await _mediator.Send(query);
			return Ok(ApiResponse<GetUsersListResponse>.Success(response));
		}

		[HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by id")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
		{
			var query = new GetUserByIdQuery { Id = id };
			var response = await _mediator.Send(query);
			return Ok(ApiResponse<GetUserByIdResponse>.Success(response));
		}

		[HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, [FromRoute] Guid id)
		{
			command.Id = id;
			var response = await _mediator.Send(command);
			return Ok(ApiResponse<UpdateUserResponse>.Success(response));
		}

		[HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete user")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
		{
			var query = new DeleteUserCommand { Id = id };
			var response = await _mediator.Send(query);
			return Ok(ApiResponse<DeleteUserResponse>.Success(response));
		}

		[HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Soft delete user")]

        public async Task<IActionResult> SoftDeleteUser([FromRoute] Guid id)
		{
			var query = new SoftDeleteUserCommand { Id = id };
			var response = await _mediator.Send(query);
			return Ok(ApiResponse<SoftDeleteUserResponse>.Success(response));
		}
	}
}
