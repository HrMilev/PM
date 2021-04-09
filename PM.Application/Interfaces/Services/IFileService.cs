using System;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IFileService
    {
        bool DeleteUserFile(string userId, string fileId);
        string GetUserFilePathIfExists(string userId, string fileId);
        Task WriteUserFileToFileSystem(string userId, string fileId, byte[] content);
    }
}