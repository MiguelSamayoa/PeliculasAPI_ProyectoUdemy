using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.IdentityModel.Tokens;

namespace PeliculasAPI_Udemy.Services
{
    public interface IFileStorage
    {
        Task<string> SaveFile(byte[] content, string extension, string container,  string contentType);

        Task<string> EditFile(byte[] content, string extension, string container, string fileRoute, string contentType);

        Task DeleteFile(string fileRoute, string container);
    }

    public class FileStorageServices : IFileStorage
    {
        private readonly string AzureConnection;
        public FileStorageServices( IConfiguration config )
        {
            AzureConnection = config.GetConnectionString("AzureStorage");
        }

        public async Task DeleteFile(string fileRoute, string container)
        {
            if (string.IsNullOrEmpty(fileRoute)) return;

            var cliente = new BlobContainerClient(AzureConnection, container);
            await cliente.CreateIfNotExistsAsync();

            var file = Path.GetFileName(fileRoute);

            var blob = cliente.GetBlobClient(file);

            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string fileRoute, string contentType)
        {
            DeleteFile(fileRoute, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var cliente = new BlobContainerClient(AzureConnection, container);
            await cliente.CreateIfNotExistsAsync();

            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var fileName = $"{Guid.NewGuid()}.{extension}";
            var blob = cliente.GetBlobClient(fileName);

            var BlobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();

            blobHttpHeader.ContentType = contentType;

            BlobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(content), BlobUploadOptions);

            return blob.Uri.ToString();
        }
    }
}
