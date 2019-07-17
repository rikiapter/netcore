

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;  
using Microsoft.WindowsAzure.Storage.Blob;   
using System;  
using System.Collections.Generic;  
using System.IO;  
using System.Threading.Tasks;
using Malam.Mastpen.HR.Core.BL.Requests;
using Microsoft.Extensions.Options;

namespace Malam.Mastpen.Core.BL.Services
{
    public class BlobStorageService
    {
        string accessKey = string.Empty;

        public BlobStorageService(IOptions<BlobConection> blobConection)
        {
            this.accessKey = blobConection.Value.AccessKey;// "DefaultEndpointsProtocol=https;AccountName=mastpenblob;AccountKey=A01qaRxdQ2LIKFs7sFyUXxLrQE+/IwR622H1CEtoI4pYEyFCk5QbOLfwRfmekSORDsuCLFNQOnU1nOqbmDMk2Q==;EndpointSuffix=core.windows.net"; 
                                                           // AppConfiguration.GetConfiguration("AccessKey");
        }

        public string UploadFileToBlob(string strFileName, FileRequest fileRequest)// byte[] fileData, string fileMimeType)
        {
            try
            {
                byte[] byteFile = Convert.FromBase64String(fileRequest.FileByte);
                var _task = Task.Run(() => this.UploadFileToBlobAsync(strFileName, byteFile, fileRequest.ContentType));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async void DeleteBlobData(string fileUrl)
        {
            Uri uriObj = new Uri(fileUrl);
            string BlobName = Path.GetFileName(uriObj.LocalPath);

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "uploads";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

            string pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
            CloudBlobDirectory blobDirectory = cloudBlobContainer.GetDirectoryReference(pathPrefix);
            // get block blob refarence    
            CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(BlobName);

            // delete blob from container        
            await blockBlob.DeleteAsync();
        }


        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }

        private async Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                string strContainerName = "uploads";
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                string fileName = this.GenerateFileName(strFileName);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                if (fileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    cloudBlockBlob.Properties.ContentType = fileMimeType;
                    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



    }
}