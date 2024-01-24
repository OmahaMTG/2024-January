using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBAC.Models;

namespace RBAC.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly Folder _folders = DemoData.RootFolder;

        [HttpGet("/[controller]/{path}")]
        [Authorize(Policy = "Reader")]
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
        [Authorize(Policy = "Writer")]
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
