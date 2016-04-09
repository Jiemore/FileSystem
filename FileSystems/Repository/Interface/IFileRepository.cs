using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;

namespace FileSystems.Repository
{
    public interface IFileRepository
    {
        File Getfile(long id);
        void UploadFiles(Files filesList, long p);
        void EditFiles(File file);
        void DeleteFiles(long fileId);
    }
}