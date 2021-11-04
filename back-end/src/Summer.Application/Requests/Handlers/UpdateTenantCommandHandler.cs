using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand>
    {
        private readonly ITenantManager _tenantManager;
        private readonly IReadRepository<Tenant> _tenantRepository;

        public UpdateTenantCommandHandler(ITenantManager tenantManager, IReadRepository<Tenant> tenantRepository)
        {
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
        }

        public async Task<Unit> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);
            if (tenant == null)
            {
                throw new NotFoundBusinessException();
            }

            await _tenantManager.UpdateAsync(tenant, request.Code, request.Name, request.ConnectionString,
                request.Host);

            return Unit.Value;
        }
    }
}