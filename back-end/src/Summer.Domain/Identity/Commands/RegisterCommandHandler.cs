using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Domain.Identity.Entities;

namespace Summer.Domain.Identity.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception("username already exists");
            }

            var newUser = new ApplicationUser() {UserName = request.UserName};
            var isCreated = await _userManager.CreateAsync(newUser, request.Password);
            if (!isCreated.Succeeded)
            {
                throw new Exception(string.Join(";", isCreated.Errors.Select(p => p.Description)));
            }

            return newUser;
        }
    }
}