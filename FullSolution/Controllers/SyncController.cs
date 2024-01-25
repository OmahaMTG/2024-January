using FullSolution.Models;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace FullSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly OpenFgaClient _fgaClient;

        public SyncController()
        {
            var configuration = new ClientConfiguration()
            {
                ApiUrl = Constants.ApiUrl,
                StoreId = Constants.StoreID,
                AuthorizationModelId = Constants.AuthorizationModelId
            };
            _fgaClient = new OpenFgaClient(configuration);
        }

        [HttpPost]
        public async Task Post()
        {
            var demoData = DemoData.RootFolder;
            await SyncFolders("|", demoData);
        }

        private async Task SyncFolders(string currentParentPath, Folder currentFolder)
        {

            while (currentParentPath.EndsWith("|"))
            {
                currentParentPath = currentParentPath.Substring(0, currentParentPath.Length - 1);
            }

            //Add Folder
            var folderPath = $"folder:{currentParentPath}|{currentFolder.Name}";
            var body = new ClientWriteRequest()
            {
                Writes = new List<ClientTupleKey>() {
                    new() {
                        User = $"folder:{(string.IsNullOrWhiteSpace(currentParentPath) ? "|" : currentParentPath)}",
                        Relation = "parent",
                        Object = folderPath
                    },
                    new() {
                        User = $"user:{currentFolder.Owner.Name}",
                        Relation = "owner",
                        Object = folderPath
                    }
                },
            };

            //Add Files
            foreach (var file in currentFolder.Files)
            {
                body.Writes.Add(
                    new()
                    {
                        User = folderPath,
                        Relation = "parent",
                        Object = $"doc:{file.Name}"
                    });
            }

            var response = await _fgaClient.Write(body);

            foreach (var subFolder in currentFolder.SubFolders)
            {
                await SyncFolders($"{currentParentPath}|{currentFolder.Name}", subFolder);
            }
        }
    }
}
