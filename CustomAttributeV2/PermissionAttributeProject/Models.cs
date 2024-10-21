namespace PermissionAttributeProject
{
    public class Models
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
    public class User : Models
    {
    }
    public class Commands : Models
    { }
}
