using Microsoft.AspNetCore.Hosting;
using PM.Application.Interfaces.Services;
using System.IO;
using System.Threading.Tasks;

namespace PM.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
            _rootPath = _env.WebRootPath;
        }

        public async Task WriteUserFileToFileSystem(string userId, string fileId, byte[] content)
        {
            var pathToDirectory = $"{_rootPath}\\{userId}";
            Directory.CreateDirectory(pathToDirectory);
            var path = pathToDirectory + $"\\{fileId}";
            using var fs = File.Create(path);
            await fs.WriteAsync(content, 0, content.Length);
        }

        public bool DeleteUserFile(string userId, string fileId)
        {
            var path = $"{_rootPath}\\{userId}\\{fileId}";

            if (!File.Exists(path))
            {
                return false;
            }

            File.Delete(path);
            return true;
        }

        public string GetUserFilePathIfExists(string userId, string fileId)
        {
            var path = $"{_rootPath}\\{userId}\\{fileId}";

            if (!File.Exists(path))
            {
                return null;
            }

            return path;
        }
    }
}
