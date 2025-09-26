using InternSotatek.Personal.Application.Common.Dtos;
using InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole;
using InternSotatek.Personal.Application.Usecases.Roles.Commands.DeleteRole;
using InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;
using InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        [SwaggerOperation(Summary = "Create new role")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(ApiResponse<CreateRoleResponse>.Success(response));
		}

		[HttpGet]
        [SwaggerOperation(Summary = "Get all roles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesListQuery query)
		{
            var response = await _mediator.Send(query);
			return Ok(ApiResponse<GetRolesListResponse>.Success(response));
		}

		[HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update role")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command, [FromRoute] Guid id)
		{
			command.Id = id;
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<UpdateRoleResponse>.Success(response));
        }

		[HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete role")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
		{
			var command = new DeleteRoleCommand { Id = id };
			var response = await _mediator.Send(command);
			return Ok(ApiResponse<DeleteRoleResponse>.Success(response));
		}
    }
}
