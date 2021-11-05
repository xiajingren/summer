using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public DeleteUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundBusinessException();
            }

            await _userRepository.DeleteAsync(user, cancellationToken);

            return Unit.Value;
        }
    }
}