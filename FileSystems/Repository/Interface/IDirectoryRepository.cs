using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;

namespace FileSystems.Repository
{
    public interface IDirectoryRepository
    {
        IQueryable<Directory> GetDirectories();
        Directory GetDirectory(long directoryId);
        IQueryable<Directory> GetChildDirectories(long directoryId);
        void DeleteDirectory(long directoryId);
        void UpdateDirectory(Directory directory);
        void CreateDirectory(Directory directory);

    }
}