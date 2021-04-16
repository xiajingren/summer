namespace Summer.App.Entities
{
    internal class SysUser : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}