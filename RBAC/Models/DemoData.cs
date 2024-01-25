namespace RBAC.Models;

public static class DemoData
{
    //Demo-Omaha@omaha.dev   : haKL^2>`Y59eO;8
    //Demo-Lincoln@omaha.dev : uY7xLQ2S8}T'3?[
    //Demo-Denver@omaha.dev  : <1'+t54&Cl}*reo
    //Demo-Chicago@omaha.dev : /xY"G4,996fN
    //Demo-Dallas@omaha.dev  : G,=9wo5gxD4n
    public static Folder RootFolder => new("Root", new User("matt@omaha.dev"))
    {
        SubFolders = new List<Folder>()
            {
                new Folder("Omaha", new User("Demo-Omaha@omaha.dev")) // 
                {
                    Files = new List<File>()
                    {
                        new File("file-Omaha-1", Guid.NewGuid().ToString()),
                        new File("file-Omaha-2", Guid.NewGuid().ToString()),
                        new File("file-Omaha-3", Guid.NewGuid().ToString()),
                        new File("file-Omaha-4", Guid.NewGuid().ToString()),
                        new File("file-Omaha-5", Guid.NewGuid().ToString()),
                        new File("file-Omaha-6", Guid.NewGuid().ToString()),
                        new File("file-Omaha-7", Guid.NewGuid().ToString()),
                        new File("file-Omaha-8", Guid.NewGuid().ToString()),
                        new File("file-Omaha-9", Guid.NewGuid().ToString())

                    }
                },
                new Folder("Lincoln", new User("Demo-Lincoln@omaha.dev"))
                {
                    Files = new List<File>()
                    {
                        new File("file-Lincoln-1", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-2", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-3", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-4", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-5", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-6", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-7", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-8", Guid.NewGuid().ToString()),
                        new File("file-Lincoln-9", Guid.NewGuid().ToString())
                    }
                },
                new Folder("Denver", new User("Demo-Denver@omaha.dev"))
                {
                    Files = new List<File>()
                    {
                        new File("file-Denver-1", Guid.NewGuid().ToString()),
                        new File("file-Denver-2", Guid.NewGuid().ToString()),
                        new File("file-Denver-3", Guid.NewGuid().ToString()),
                        new File("file-Denver-4", Guid.NewGuid().ToString()),
                        new File("file-Denver-5", Guid.NewGuid().ToString()),
                        new File("file-Denver-6", Guid.NewGuid().ToString()),
                        new File("file-Denver-7", Guid.NewGuid().ToString()),
                        new File("file-Denver-8", Guid.NewGuid().ToString()),
                        new File("file-Denver-9", Guid.NewGuid().ToString())
                    }
                }
            }
    };
}