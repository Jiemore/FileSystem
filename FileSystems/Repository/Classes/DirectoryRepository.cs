using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;
using System.Data.Entity;
using FileSystems.DAL;


namespace FileSystems.Repository
{
    public class DirectoryRepository : IDirectoryRepository
    {
        private readonly DirectoryContext db;

        public DirectoryRepository()
        {
            db = new DirectoryContext();
        }

        public IQueryable<Directory> GetDirectories()
        {
            return db.Directories;
        }

        public Directory GetDirectory(long directoryId)
        {
            return db.Directories.Find(directoryId);
        }

        public IQueryable<Directory> GetChildDirectories(long directoryId)
        {
            return db.Directories.Where(x => x.ParentId == directoryId);
        }

        public void DeleteDirectory(long directoryId)
        {
            var directory = db.Directories.Find(directoryId);
            var childrenDirectory = db.Directories.ToList().FindAll(x => x.ParentId == directoryId);
            var filesinDirectory = directory.File.ToList();
            db.Directories.Remove(directory);

            foreach (var item in filesinDirectory)
            {
                db.Files.Remove(item);
            };

            foreach (var item in childrenDirectory)
            {
                db.Directories.Remove(item);
            };
            
            CommitChanges();
        }

        public void UpdateDirectory(Directory directory)
        {
            db.Entry(directory).State = System.Data.EntityState.Modified;
            CommitChanges();
        }

        public void CreateDirectory(Directory directory)
        {
            db.Directories.Add(directory);
            CommitChanges();
        }

        private void CommitChanges()
        {
            db.SaveChanges();     
        }
       
    }
}