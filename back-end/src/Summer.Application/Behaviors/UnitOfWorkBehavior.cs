using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.UnitOfWork;

namespace Summer.Application.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var unitOfWorkAttr = request.GetType().GetCustomAttribute<UnitOfWorkAttribute>();
            if (unitOfWorkAttr == null) return await next();

            await _unitOfWork.BeginAsync(cancellationToken);

            var response = await next();

            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}