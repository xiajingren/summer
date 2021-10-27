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
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IReadRepository<User> _userRepository;

        public UpdateUserPasswordCommandHandler(IUserManager userManager, IReadRepository<User> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundBusinessException();
            }

            await _userManager.UpdatePasswordAsync(user, request.Password);

            return Unit.Value;
        }
    }
}