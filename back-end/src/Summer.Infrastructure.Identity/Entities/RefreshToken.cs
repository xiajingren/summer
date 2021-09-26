using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Summer.Infrastructure.Identity.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string JwtId { get; set; }

        [Required]
        [StringLength(256)]
        public string Token { get; set; }

        [Required]
        public bool Used { get; set; }

        /// <summary>
        /// 是否失效。修改用户信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        [Required]
        public bool Invalidated { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime ExpiryTime { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}