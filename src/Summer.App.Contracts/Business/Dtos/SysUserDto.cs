using System;

namespace Summer.App.Contracts.Business.Dtos
{
    public class SysUserDto
    {
        public Guid? Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public Guid? AvatarId { get; set; }

        public UploadFileOutputDto Avatar { get; set; }
    }
}
