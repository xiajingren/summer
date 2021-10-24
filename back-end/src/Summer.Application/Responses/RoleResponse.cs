namespace Summer.Application.Responses
{
    public class RoleResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RoleResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}