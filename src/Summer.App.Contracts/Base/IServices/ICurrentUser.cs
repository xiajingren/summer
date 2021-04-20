using System;
using Summer.App.Contracts.Base.Dtos;

namespace Summer.App.Contracts.Base.IServices
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public interface ICurrentUser
    {
        public bool IsLogin { get; }

        public Guid? Id { get; }

        CurrentUserDto ToDto();
    }
}
