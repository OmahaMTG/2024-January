namespace _1_Simple_AuthN.Models;

public class Folder(string name)
{
    public string Name { get; set; } = name;
    public IEnumerable<Folder> SubFolders { get; set; } = Enumerable.Empty<Folder>();
    public IEnumerable<File> Files { get; set; } = Enumerable.Empty<File>();

}