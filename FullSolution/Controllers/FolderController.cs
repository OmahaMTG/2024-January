using Fga.Net.AspNetCore.Authorization;
using Fga.Net.AspNetCore.Authorization.Attributes;
using FullSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullSolution.Controllers
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

            var folder = FindFolder(path, "|Root", _folders);
            if (folder == null)
                return NotFound();

            return Ok(folder);
        }

        [HttpPost("/[controller]/{path}")]
        //[Authorize(Policy = "Reader")]
        [FgaRouteObject("owner", "folder", nameof(path))]
        public IActionResult Post(string path)

        {
            if (!path.StartsWith("|"))
                throw new ArgumentException("The path must begin with a '|'");

            var folder = FindFolder(path, "|Root", _folders);
            if (folder == null)
                return NotFound();

            return Ok(folder);
        }

        private Folder? FindFolder(string searchFolderPath, string builtFolderPath, Folder currentFolder)
        {
            if (searchFolderPath == builtFolderPath)
                return currentFolder;

            foreach (var folder in currentFolder.SubFolders)
            {
                var folderData = $"{builtFolderPath}|{folder.Name}";
                var returnedFolder = FindFolder(searchFolderPath, $"{builtFolderPath}|{folder.Name}", folder);
                if (returnedFolder != null)
                    return returnedFolder;
            }

            return null;
        }
    }
}
