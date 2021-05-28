using System.ComponentModel.DataAnnotations;
using Summer.App.Base.Entities;

namespace Summer.App.Business.Entities
{
    /// <summary>
    /// 上传文件
    /// </summary>
    internal class UploadFile : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }
    }
}