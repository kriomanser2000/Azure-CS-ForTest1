using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ForTest1
{
    class Program
    {
        static async Task Main()
        {
            //Отримання рядка підключення

            Console.WriteLine("Azure Blob storage v12 - .NET quickstart sample\n");
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            //Створення контейнера

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            //Надсилання великих двійкових об'єктів у контейнер

            string localPath = "./data/";
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            await File.WriteAllTextAsync(localFilePath, "Hello, sasa hohol.");
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Upload to Blob storage as Blob:\n\t {0}\n", blobClient.Uri);
            using FileStream uploadFileStream = File.OpenWrite(localFilePath);
            await blobClient.UploadAsync(uploadFileStream);
            uploadFileStream.Close();

            //Скачування великих двійкових об'єктів

            string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOAD.txt");
            Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            using FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
            await download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();
        }
    }
}