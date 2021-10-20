using FluentValidation;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Validators
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("角色名称不能为空");
        }
    }
}