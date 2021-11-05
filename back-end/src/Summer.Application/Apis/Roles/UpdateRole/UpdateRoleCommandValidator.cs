using FluentValidation;

namespace Summer.Application.Apis.Roles.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("角色名称不能为空");
        }
    }
}