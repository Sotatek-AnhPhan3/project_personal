
using MediatR;

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUserById
{
	public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
	{
		public Guid Id { get; set; }
	}
}
