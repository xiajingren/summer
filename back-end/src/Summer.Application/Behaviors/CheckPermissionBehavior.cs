using System.Linq;
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
            var attr = request.GetType().GetCustomAttributes(typeof(PermissionAttribute), false).FirstOrDefault();
            if (attr != null)
            {
                var permissionAttr = attr as PermissionAttribute;

                // todo:
            }

            return await next();
        }
    }
}