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
                        new File("file-Omaha-1"),
                        new File("file-Omaha-2"),
                        new File("file-Omaha-3"),
                        new File("file-Omaha-4"),
                        new File("file-Omaha-5"),
                        new File("file-Omaha-6"),
                        new File("file-Omaha-7"),
                        new File("file-Omaha-8"),
                        new File("file-Omaha-9")

                    }
                },
                new Folder("Lincoln")
                {
                    Files = new List<File>()
                    {
                        new File("file-Lincoln-1"),
                        new File("file-Lincoln-2"),
                        new File("file-Lincoln-3"),
                        new File("file-Lincoln-4"),
                        new File("file-Lincoln-5"),
                        new File("file-Lincoln-6"),
                        new File("file-Lincoln-7"),
                        new File("file-Lincoln-8"),
                        new File("file-Lincoln-9")
                    }
                },
                new Folder("Denver")
                {
                    Files = new List<File>()
                    {
                        new File("file-Denver-1"),
                        new File("file-Denver-2"),
                        new File("file-Denver-3"),
                        new File("file-Denver-4"),
                        new File("file-Denver-5"),
                        new File("file-Denver-6"),
                        new File("file-Denver-7"),
                        new File("file-Denver-8"),
                        new File("file-Denver-9")
                    }
                }
            }
        };
    }
}
