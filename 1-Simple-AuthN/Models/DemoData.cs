namespace _1_Simple_AuthN.Models
{
    public static class DemoData
    {
        public static Folder RootFolder => new("Root")
        {
            SubFolders = new List<Folder>()
            {
                new Folder("Omaha")
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
                new Folder("Lincoln")
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
                new Folder("Denver")
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
}
