using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Commands;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateCurrentUserProfileCommandHandler : IRequestHandler<UpdateCurrentUserProfileCommand>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserManager _userManager;

        public UpdateCurrentUserProfileCommandHandler(ICurrentUser currentUser, IUserManager userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateCurrentUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedBusinessException();
            }

            var user = await _currentUser.GetUserAsync();
            await _userManager.UpdateAsync(user, request.UserName, user.RoleIds);

            return Unit.Value;
        }
    }
}