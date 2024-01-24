namespace RBAC.Models
{
    public class File(string name, string content)
    {
        public string Name { get; set; } = name;
        public string Content { get; set; } = content;
    }
}
