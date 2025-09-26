using InternSotatek.Personal.Application.Common.Dtos;
using InternSotatek.Personal.Application.Usecases.UserRoles.Commands.Create;
using InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InternSotatek.Personal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserRolesController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new user role")]
        public async Task<IActionResult> CreateUserRoles(CreateUserRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<CreateUserRoleResponse>.Success(response));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get role user by user id")]
        public async Task<IActionResult> GetAllRoleByUser(Guid id)
        {
            var query = new GetAllRoleByUserIdQuery { UserId = id };
            var response = await _mediator.Send(query);

            return Ok(ApiResponse<GetAllRoleByUserIdResponse>.Success(response));
        }
    }
}
