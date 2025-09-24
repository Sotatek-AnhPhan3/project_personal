using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
