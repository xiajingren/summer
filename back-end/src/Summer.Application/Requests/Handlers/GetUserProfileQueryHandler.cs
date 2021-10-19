using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(ICurrentUser currentUser, IMapper mapper)
        {
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public Task<UserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<UserProfileResponse>(_currentUser);
            return Task.FromResult(response);
        }
    }
}