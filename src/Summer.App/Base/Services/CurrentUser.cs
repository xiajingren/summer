using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;

namespace Summer.App.Base.Services
{
    internal class CurrentUser : ICurrentUser
    {
        private ClaimsPrincipal ClaimsPrincipal { get; }

        public CurrentUser(IServiceProvider serviceProvider)
        {
            var principal = serviceProvider.GetService<IPrincipal>();
            ClaimsPrincipal = (principal as ClaimsPrincipal);
        }

        public bool IsLogin => Id.HasValue;

        public Guid? Id
        {
            get
            {
                var id = ClaimsPrincipal?.FindFirst("id");
                return id == null ? (Guid?)null : Guid.Parse(id.ToString());
            }
        }

        public CurrentUserDto ToDto()
        {
            throw new NotImplementedException();
        }

    }
}