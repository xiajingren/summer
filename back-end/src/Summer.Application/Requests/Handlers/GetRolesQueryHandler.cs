using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Handlers
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginationResponse<RoleResponse>>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<RoleResponse>> Handle(GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            var rowCount = await _roleManager.Roles.CountAsync(cancellationToken);
            var roles = _roleManager.Roles.Skip(request.GetSkip()).Take(request.PageSize).ToList();

            return new PaginationResponse<RoleResponse>(request.PageIndex, request.PageSize, rowCount,
                _mapper.Map<IEnumerable<RoleResponse>>(roles));
        }
    }
}