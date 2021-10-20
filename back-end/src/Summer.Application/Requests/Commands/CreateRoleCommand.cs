using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class CreateRoleCommand : IRequest<RoleResponse>
    {
        public string Name { get; set; }

        public CreateRoleCommand(string name)
        {
            Name = name;
        }
    }
}