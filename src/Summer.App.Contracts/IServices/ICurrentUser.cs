using Summer.App.Contracts.Dtos;
using System;

namespace Summer.App.Contracts.IServices
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
