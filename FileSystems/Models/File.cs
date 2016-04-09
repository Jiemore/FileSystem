using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileSystems.Models
{
    [Table("File")]
    public class File
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string Name { get; set; }

        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }

        [Display(Name = "File Size")]
        public int Size { get; set; }

        public virtual Directory Directory { get; set; }

        public static File CreateModel(ViewModel.FileViewModel fileViewModel)
        {
            return new File()
            {
                Id = fileViewModel.Id,
                CreatedAt = fileViewModel.CreatedAt,
                LastModifiedAt = DateTime.Now,
                Name = fileViewModel.Name,
                Extension = fileViewModel.Extension,
                Size = fileViewModel.Size
            };

        }
        
        public void DeleteFileFromServer()
        {
            var targetPath = GetFileServerPath();
            if (!System.IO.Directory.Exists(targetPath))
            {                
                System.IO.File.Delete(targetPath);
            }
        }

        public  byte[] GetFileFromServer()
        {
            var targetPath = GetFileServerPath();
            return System.IO.File.ReadAllBytes(targetPath);            
        }

        public void UpdateFileOnServer(string targetName)
        {
            var existingFile = System.IO.Path.Combine(GetServerFolder(), targetName);
            var targetPath = GetFileServerPath();
            System.IO.File.Copy(existingFile, targetPath);
            System.IO.File.Delete(existingFile);
        }

        private string GetServerFolder()
        {
            return HttpContext.Current.Server.MapPath("~/DAL/UploadedFiles");
        }

        private string GetFileServerPath()
        {
            var targetFolder = GetServerFolder();
            var fileName = Name;
            var targetPath = System.IO.Path.Combine(targetFolder, fileName);
            return targetPath;
        }

        
    }

    public class Files : List<File>
    {
        public Files(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var uploadedFile in files)
            {
                if (uploadedFile == null)
                    continue;

                if (uploadedFile.ContentLength > 0)
                {
                    this.Add(Files.CreateModel(uploadedFile));
                }
            }

        }

        private static File CreateModel(HttpPostedFileBase uploadedFile)
        {
            var file = new File();
            var fileName = uploadedFile.FileName.Split('\\').Last();
            var targetFolder = HttpContext.Current.Server.MapPath("~/DAL/UploadedFiles");
            var targetPath = System.IO.Path.Combine(targetFolder, fileName);
            file.Extension = uploadedFile.ContentType;
            file.Name = fileName;
            file.Size = uploadedFile.ContentLength;
            file.CreatedAt = DateTime.Now;
            file.LastModifiedAt = DateTime.Now;

            if (!System.IO.Directory.Exists(targetPath))
            {
                uploadedFile.SaveAs(targetPath);
            }

            return file;
        }
    }
}