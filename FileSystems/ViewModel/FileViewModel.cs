using FileSystems.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystems.ViewModel
{
    public class FileViewModel
    {
        public int Id { get; set; }

        public long ParentId { get; set; }

        [Display(Name = "File Name")]
        public string Name { get; set; }

        [Display(Name = "Created @")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified @")]
        public DateTime LastModifiedAt { get { return DateTime.Now; } }

        [Display(Name = "Ext*")]
        public string Extension { get; set; }

        [Display(Name = "size")]
        public int Size { get; set; }

        public static FileViewModel CreateViewModel(File model)
        {
            return new FileViewModel()
            {
                Id = model.Id,
                CreatedAt = model.CreatedAt,
                Extension = model.Extension,                
                Name = model.Name,
                ParentId = model.Directory.Id,
                Size = model.Size
            };
        }

        public static File CreateModel(HttpPostedFileBase uploadedFile, long parentId)
        {
            var file = new File();
            var fileName = uploadedFile.FileName.Split('\\').Last();
            string targetFolder = HttpContext.Current.Server.MapPath("~/DAL/UploadedFiles");
            string targetPath = System.IO.Path.Combine(targetFolder, fileName);                   
            file.Extension = uploadedFile.ContentType;
            file.Name = fileName;
            file.Size = uploadedFile.ContentLength;
            file.CreatedAt = DateTime.Now;
            file.LastModifiedAt = DateTime.Now;
            file.Directory.Id = parentId;
            if (!System.IO.Directory.Exists(targetPath))
            {
                uploadedFile.SaveAs(targetPath);                 
            }

            return file;
        }
       
        public static IEnumerable<FileViewModel> CreateListViewModel(ICollection<File> files)
        {
            return files.Select(x => FileViewModel.CreateViewModel(x)).ToList();  
        }

    }
}