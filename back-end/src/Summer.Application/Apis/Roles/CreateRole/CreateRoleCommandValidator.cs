using FluentValidation;

namespace Summer.Application.Apis.Roles.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("角色名称不能为空");
        }
    }
}