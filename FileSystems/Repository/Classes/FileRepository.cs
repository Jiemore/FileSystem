using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;
using FileSystems.DAL;
using System.Web.ModelBinding;
using System.Data.Entity.Infrastructure;

namespace FileSystems.Repository
{
    public class FileRepository: IFileRepository
    {
        private readonly DirectoryContext db;

        public FileRepository()
        {
            db = new DirectoryContext();
        }

        public File Getfile(long id)
        {
            return db.Files.ToList().Find(x => x.Id == id);
        }

        public void UploadFiles(Files filesList, long id = 0)
        {
            var directory = GetDirectory(id);
            if (directory == null)
                throw new Exception("Directory is Invalid");

            var filesOnServer = directory.File.ToDictionary(r => r.Name);
            try
            {
                foreach (var file in filesList)
                {
                    if (!filesOnServer.ContainsKey(file.Name))
                    {
                        directory.File.Add(file);
                        db.Entry(file).State = System.Data.EntityState.Added;
                    }
                                        
                }
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

        }        

        public void EditFiles(File file)
        {            
            var serverFile = db.Files.Find(file.Id);
            if(!serverFile.Name.Equals(file.Name))
            {
                serverFile.Name = file.Name;
            }
            db.Entry(serverFile).State = System.Data.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }                   
        }

        public void DeleteFiles(long fileId)
        {
            var file = db.Files.ToList().Find(x => x.Id == fileId);
            db.Files.Remove(file);
            db.SaveChanges();
        }

        private Directory GetDirectory(long id)
        {
            return db.Directories.ToList().Find(x => x.Id == id);
        }

        private void CommitChanges()
        {
            db.SaveChanges();
        }

    }
}