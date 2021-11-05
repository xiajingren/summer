using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Summer.Application.Apis.Tenants.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantResponse>
    {
        public Task<TenantResponse> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}