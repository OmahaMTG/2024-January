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
        private OpenFgaClient _fgaClient;

        public SyncController()
        {
            var configuration = new ClientConfiguration()
            {
                ApiUrl = "http://localhost:8080",
                StoreId = "01HK63XHDNKS77T0V6QBP73R1N",
                AuthorizationModelId = "01HMVKFSBPJSVR74M6HP3GCFZE"
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

            var body = new ClientWriteRequest()
            {
                Writes = new List<ClientTupleKey>() {
                    // the notes folder is a parent of the meeting notes document
                    new() {
                        User = $"folder:{currentParentPath}|{currentFolder.Name}",
                        Relation = "parent",
                        Object = $"folder:{(string.IsNullOrWhiteSpace(currentParentPath) ? "|" : currentParentPath)}"
                    }
                },
            };

            foreach (var file in currentFolder.Files)
            {
                body.Writes.Add(
                    new()
                    {
                        User = $"folder:{currentParentPath}|{currentFolder.Name}",
                        Relation = "parent",
                        Object = $"document:{file.Name}"
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
