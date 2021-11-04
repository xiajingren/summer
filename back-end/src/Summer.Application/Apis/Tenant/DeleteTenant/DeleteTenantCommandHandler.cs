using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Tenant.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand>
    {
        private readonly IRepository<Domain.Entities.Tenant> _tenantRepository;

        public DeleteTenantCommandHandler(IRepository<Domain.Entities.Tenant> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);
            if (tenant == null)
            {
                throw new NotFoundBusinessException();
            }

            await _tenantRepository.DeleteAsync(tenant, cancellationToken);

            return Unit.Value;
        }
    }
}