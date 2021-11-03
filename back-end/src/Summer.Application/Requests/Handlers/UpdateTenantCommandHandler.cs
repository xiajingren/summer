using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand>
    {
        public Task<Unit> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}