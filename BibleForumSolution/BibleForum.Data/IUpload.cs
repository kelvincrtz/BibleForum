using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibleForum.Data
{
    public interface IUpload
    {
        CloudBlobContainer GetCloudBlobContainer(string connectionString);
    }
}
