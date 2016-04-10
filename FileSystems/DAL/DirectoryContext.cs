using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FileSystems.Models;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace FileSystems.DAL
{
    public class DirectoryContext : DbContext
    {
        public DbSet<Directory> Directories { get; set; }
        public DbSet<File> Files { get; set; }

        public DirectoryContext()
            : base("DirectoryContext")
        {
            Database.SetInitializer<DirectoryContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}