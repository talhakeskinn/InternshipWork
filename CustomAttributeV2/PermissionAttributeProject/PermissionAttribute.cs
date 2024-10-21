
namespace PermissionAttributeProject
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class PermissionAttribute : Attribute
    {
        public string CommandName { get; set; }
        public PermissionAttribute(string CommandName)
        {
            this.CommandName = CommandName;
        }

    }
}
