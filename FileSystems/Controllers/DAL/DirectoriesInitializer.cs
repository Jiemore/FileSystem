using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;

namespace FileSystems.DAL
{
    public class DirectoriesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DirectoryContext>
    {
        protected override void Seed(DirectoryContext context)
        {
            var directories= new List<Directory>();
            directories.Add(new Directory() { Id = 1, Name = "root", CreatedAt = DateTime.Now });
            directories.Add(new Directory() { Id = 2, Name = "InRule Root", CreatedAt = DateTime.Now });
            directories.Add(new Directory() { Id = 2, Name = "$ Root", CreatedAt = DateTime.Now });

            directories.ForEach(x => context.Directories.Add(x));
            context.SaveChanges();
            
        }
    }
}