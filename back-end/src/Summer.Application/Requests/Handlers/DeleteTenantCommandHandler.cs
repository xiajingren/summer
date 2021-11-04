using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;

namespace Summer.Application.Requests.Handlers
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand>
    {
        private readonly IRepository<Tenant> _tenantRepository;

        public DeleteTenantCommandHandler(IRepository<Tenant> tenantRepository)
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