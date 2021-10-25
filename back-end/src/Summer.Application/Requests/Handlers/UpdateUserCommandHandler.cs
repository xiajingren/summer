using System.Linq;
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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _userRepository;

        public UpdateUserCommandHandler(IUserManager userManager, IRepository<User> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundBusinessException();
            }

            user.SetRoles(request.Roles.Select(x => x.Id));

            await _userManager.UpdateAsync(user, request.UserName, request.Password);

            return Unit.Value;
        }
    }
}