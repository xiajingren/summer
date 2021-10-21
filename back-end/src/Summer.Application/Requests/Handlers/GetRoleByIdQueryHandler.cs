using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Exceptions;

namespace Summer.Application.Requests.Handlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleResponse>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new NotFoundBusinessException();
            }

            return _mapper.Map<RoleResponse>(role);
        }
    }
}