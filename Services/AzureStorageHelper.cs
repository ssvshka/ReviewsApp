using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;

namespace CourseProject.Services
{
    public class AzureStorageHelper
    {
        private string baseUrl;
        private string connectionString;
        private string containerName;
        public AzureStorageHelper(IConfiguration configuration)
        {
            baseUrl = configuration["StorageBaseUrl"]!;
            connectionString = configuration["StorageConnectionString"]!;
            containerName = configuration["ContainerName"]!;
        }

        public string GetUrlName(string fileName)
        {
            var container = OpenContainer();
            if (container == null) return "";
            return $"{baseUrl}{containerName}/{fileName}";
        }

        public async Task UploadImageAsync(string filename, IBrowserFile file)
        {
            var container = OpenContainer();
            await container.UploadBlobAsync(
                filename, file.OpenReadStream());
        }

        private BlobContainerClient OpenContainer()
            => new BlobServiceClient(connectionString)
                .GetBlobContainerClient(containerName);
    }
}
