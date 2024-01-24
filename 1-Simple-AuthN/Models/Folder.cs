namespace _1_Simple_AuthN.Models;

public class Folder(string name, User owner)
{
    public string Name { get; set; } = name;
    public IEnumerable<Folder> SubFolders { get; set; } = Enumerable.Empty<Folder>();
    public IEnumerable<File> Files { get; set; } = Enumerable.Empty<File>();
    public User Owner { get; set; } = owner;
    public List<Group> Groups { get; set; } = new List<Group>();
}

public class User(string name)
{
    public string Name { get; set; } = name;
}

public class Group(string name)
{
    public string Name { get; set; } = name;
}