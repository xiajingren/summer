using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Domain.Interfaces;

namespace Summer.Application.Apis.Tenant.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, TenantResponse>
    {
        private readonly ITenantManager _tenantManager;
        private readonly IMapper _mapper;

        public CreateTenantCommandHandler(ITenantManager tenantManager, IMapper mapper)
        {
            _tenantManager = tenantManager;
            _mapper = mapper;
        }

        public async Task<TenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant =
                await _tenantManager.CreateAsync(request.Code, request.Name, request.ConnectionString, request.Host);

            return _mapper.Map<TenantResponse>(tenant);
        }
    }
}