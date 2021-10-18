using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Summer.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string JwtId { get; set; }

        public string Token { get; set; }

        public bool Used { get; set; }

        /// <summary>
        /// 是否失效。修改用户信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        public bool Invalidated { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ExpiryTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}