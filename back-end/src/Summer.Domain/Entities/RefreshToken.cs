using System;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;
using Summer.Shared.Utils;

namespace Summer.Domain.Entities
{
    public class RefreshToken : BaseEntity, IAggregateRoot
    {
        public string JwtId { get; private set; }

        public string Token { get; private set; }

        public bool Used { get; private set; }

        /// <summary>
        /// 是否失效。修改用户信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        public bool Invalidated { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime ExpiredAt { get; private set; } = DateTime.UtcNow.AddMonths(6);

        public int UserId { get; private set; }

        private RefreshToken()
        {
            // required by EF
        }

        public RefreshToken(string jwtId, int userId)
        {
            JwtId = jwtId;
            UserId = userId;
            Token = CommonHelper.GenerateRandomNumber();
        }

        public void Invalidate()
        {
            Invalidated = true;
        }

        public void Confirm(string jwtId)
        {
            if (ExpiredAt < DateTime.UtcNow)
            {
                throw new BusinessException("refresh_token已过期...");
            }

            if (Invalidated)
            {
                throw new BusinessException("refresh_token已失效...");
            }

            if (Used)
            {
                throw new BusinessException("refresh_token已使用...");
            }

            if (JwtId != jwtId)
            {
                throw new BusinessException("refresh_token与此token不匹配...");
            }

            Used = true;
        }
    }
}