using MediatR;

namespace Summer.Application.Requests.Commands
{
    public class DeleteRoleCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}