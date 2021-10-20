using MediatR;

namespace Summer.Application.Requests.Commands
{
    public class UpdateRoleCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UpdateRoleCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}