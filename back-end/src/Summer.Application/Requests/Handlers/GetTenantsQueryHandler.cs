using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Requests.Handlers
{
    public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, PaginationResponse<TenantResponse>>
    {
        private readonly IReadRepository<Tenant> _tenantRepository;
        private readonly IMapper _mapper;

        public GetTenantsQueryHandler(IReadRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<TenantResponse>> Handle(GetTenantsQuery request,
            CancellationToken cancellationToken)
        {
            var rowCount = await _tenantRepository.CountAsync(new TenantSpec(request.Filter), cancellationToken);
            var roles = await _tenantRepository.ListAsync(
                new TenantSpec(request.Filter, request.GetSkip(), request.PageSize),
                cancellationToken);

            return new PaginationResponse<TenantResponse>(request.PageIndex, request.PageSize, rowCount,
                _mapper.Map<IEnumerable<TenantResponse>>(roles));
        }
    }
}