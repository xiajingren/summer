using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Exceptions;

namespace Summer.Application.Requests.Handlers
{
    public class GetCurrentUserProfileQueryHandler : IRequestHandler<GetCurrentUserProfileQuery, CurrentUserProfileResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetCurrentUserProfileQueryHandler(ICurrentUser currentUser, IMapper mapper)
        {
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public Task<CurrentUserProfileResponse> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedBusinessException();
            }

            var response = _mapper.Map<CurrentUserProfileResponse>(_currentUser);
            return Task.FromResult(response);
        }
    }
}