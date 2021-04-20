using Summer.App.Base.Entities;

namespace Summer.App.Business.Entities
{
    internal class SysUser : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}