using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Commands;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateCurrentUserPasswordCommandHandler : IRequestHandler<UpdateCurrentUserPasswordCommand>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserManager _userManager;

        public UpdateCurrentUserPasswordCommandHandler(ICurrentUser currentUser, IUserManager userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateCurrentUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedBusinessException();
            }

            var user = await _currentUser.GetUserAsync();
            var passed = _userManager.CheckPassword(user, request.Password);
            if (!passed)
            {
                throw new BusinessException("旧密码错误");
            }

            await _userManager.UpdatePasswordAsync(user, request.NewPassword);

            return Unit.Value;
        }
    }
}