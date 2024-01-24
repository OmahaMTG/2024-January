using _1_Simple_AuthN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace _1_Simple_AuthN.Controllers
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
            var body = new ClientWriteRequest()
            {
                Writes = new List<ClientTupleKey>() {
                    // the notes folder is a parent of the meeting notes document
                    new() {
                        User = $"folder:{currentParentPath}|{currentFolder.Name}",
                        Relation = "parent",
                        Object = $"folder:{(string.IsNullOrWhiteSpace(currentParentPath) ? "|" : currentParentPath)}"
                    },
                    new() {
                        User = $"user:{currentFolder.Owner.Name}",
                        Relation = "owner",
                        Object = $"folder:{(string.IsNullOrWhiteSpace(currentParentPath) ? "|" : currentParentPath)}"
                    }
                },
            };

            //Add Files
            foreach (var file in currentFolder.Files)
            {
                body.Writes.Add(
                    new()
                    {
                        User = $"folder:{currentParentPath}|{currentFolder.Name}",
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
