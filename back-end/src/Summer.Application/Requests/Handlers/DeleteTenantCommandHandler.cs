using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Handlers
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand>
    {
        public Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}