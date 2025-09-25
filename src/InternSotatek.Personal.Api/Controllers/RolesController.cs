using InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole;
using InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;
using InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> CreateRole(CreateRoleCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesListQuery query)
		{
            var response = await _mediator.Send(query);
			return Ok(response);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command, [FromRoute] Guid id)
		{
			command.Id = id;
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
