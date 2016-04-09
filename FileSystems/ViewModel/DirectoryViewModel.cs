using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystems.Models;
using System.ComponentModel.DataAnnotations;

namespace FileSystems.ViewModel
{
    public class DirectoryViewModel
    { 
        public long Id { get; set; }

        [Display(Name= "Directory" )]
        public string Name { get; set; }

        [Display(Name = "Created @")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified @")]
        public DateTime ModifiedAt { get { return DateTime.Now; } }

        public long ParentId { get; set; }

        public static List<DirectoryViewModel> CreateViewModelList(List<Directory> models)
        {
            return models.Select(x => new DirectoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                ParentId = x.ParentId,
            }).ToList();
        }

        public static DirectoryViewModel CreateViewModel(Directory model)
        {
            return new DirectoryViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = model.CreatedAt,
                ParentId = model.ParentId
            };
        }

    }
}