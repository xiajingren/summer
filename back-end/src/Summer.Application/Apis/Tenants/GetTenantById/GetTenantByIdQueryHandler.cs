using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Tenants.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantResponse>
    {
        private readonly IReadRepository<Tenant> _tenantRepository;
        private readonly IMapper _mapper;

        public GetTenantByIdQueryHandler(IReadRepository<Tenant> tenantRepository,IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }
        
        public async Task<TenantResponse> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);
            if (tenant == null)
            {
                throw new NotFoundBusinessException();
            }

            return _mapper.Map<TenantResponse>(tenant);
        }
    }
}