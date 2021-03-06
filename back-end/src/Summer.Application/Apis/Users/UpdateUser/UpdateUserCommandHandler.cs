using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IReadRepository<User> _userRepository;

        public UpdateUserCommandHandler(IUserManager userManager, IReadRepository<User> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundBusinessException();
            }

            await _userManager.UpdateAsync(user, request.UserName, request.RoleIds);

            return Unit.Value;
        }
    }
}