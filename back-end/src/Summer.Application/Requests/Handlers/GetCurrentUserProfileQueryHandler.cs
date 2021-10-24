using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

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
            var response = _mapper.Map<CurrentUserProfileResponse>(_currentUser);
            return Task.FromResult(response);
        }
    }
}