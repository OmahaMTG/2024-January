using _1_Simple_AuthN.Models;
using Fga.Net.AspNetCore.Authorization;
using Fga.Net.AspNetCore.Authorization.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1_Simple_AuthN.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    [Authorize(FgaAuthorizationDefaults.PolicyKey)]
    public class FolderController : ControllerBase
    {
        private readonly Folder _folders = DemoData.RootFolder;
        
        [HttpGet("/[controller]/{path}")]
        //[Authorize(Policy = "Reader")]
        [FgaRouteObject("viewer", "folder", nameof(path))]
        public IActionResult Get(string path)
        
        {
            if (!path.StartsWith("|"))
                throw new ArgumentException("The path must begin with a '|'");

            var foldersInPath = path.Split("|");
            var currentFolder = _folders;
            foreach (var folderName in foldersInPath)
            {
                if (string.IsNullOrWhiteSpace(folderName))
                    continue;
                currentFolder = currentFolder.SubFolders.SingleOrDefault(f => f.Name == folderName);
                if (currentFolder == null)
                    return NotFound(folderName);

            }

            return Ok(currentFolder);
        }

        [HttpPost]
        //[Authorize(Policy = "Writer")]
        public string Post()
        {
            return "You are a writer";
        }
    }
}
