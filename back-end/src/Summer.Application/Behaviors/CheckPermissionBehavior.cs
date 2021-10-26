using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Permissions;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;

namespace Summer.Application.Behaviors
{
    public class CheckPermissionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IPermissionManager _permissionManager;
        private readonly ICurrentUser _currentUser;

        public CheckPermissionBehavior(IPermissionManager permissionManager, ICurrentUser currentUser)
        {
            _permissionManager = permissionManager;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var permissionAttr = request.GetType().GetCustomAttribute<PermissionAttribute>();
            if (permissionAttr == null) return await next();

            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedBusinessException();
            }

            var passed = await _permissionManager.CheckUserPermissionAsync(_currentUser.Id, permissionAttr.Code);

            if (!passed)
            {
                throw new ForbidBusinessException();
            }

            return await next();
        }
    }
}