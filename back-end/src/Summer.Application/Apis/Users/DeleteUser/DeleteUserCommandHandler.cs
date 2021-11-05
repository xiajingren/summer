using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Summer.Application.Apis.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        public Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}