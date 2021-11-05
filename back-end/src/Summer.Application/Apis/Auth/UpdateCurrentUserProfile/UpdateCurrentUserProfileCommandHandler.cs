using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Auth.UpdateCurrentUserProfile
{
    public class UpdateCurrentUserProfileCommandHandler : IRequestHandler<UpdateCurrentUserProfileCommand>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _userRepository;

        public UpdateCurrentUserProfileCommandHandler(ICurrentUser currentUser, IUserManager userManager,
            IRepository<User> userRepository)
        {
            _currentUser = currentUser;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateCurrentUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedBusinessException();
            }

            var user = await _userRepository.GetByIdAsync(_currentUser.Id, cancellationToken);
            await _userManager.UpdateAsync(user, request.UserName, user.RoleIds);

            return Unit.Value;
        }
    }
}