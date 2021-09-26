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
        [StringLength(50)]
        public string JwtId { get; set; }

        [Required]
        [StringLength(100)]
        public string Token { get; set; }

        [Required]
        public bool Used { get; set; }

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