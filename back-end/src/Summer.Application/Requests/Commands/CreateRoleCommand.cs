using MediatR;

namespace Summer.Application.Requests.Commands
{
    public class CreateRoleCommand : IRequest
    {
        public string Name { get; set; }

        public CreateRoleCommand(string name)
        {
            Name = name;
        }
    }
}