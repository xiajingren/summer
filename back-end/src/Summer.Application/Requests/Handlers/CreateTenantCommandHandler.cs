using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Handlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, TenantResponse>
    {
        public Task<TenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}