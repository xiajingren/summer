using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Handlers
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantResponse>
    {
        public Task<TenantResponse> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}