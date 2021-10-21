using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Permissions;

namespace Summer.Application.Behaviors
{
    public class CheckPermissionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var permissionAttr = request.GetType().GetCustomAttribute<PermissionAttribute>();
            if (permissionAttr != null)
            {
                // todo:
            }

            return await next();
        }
    }
}