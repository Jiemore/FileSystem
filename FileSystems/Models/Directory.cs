using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FileSystems.ViewModel;

namespace FileSystems.Models
{
    [Table("Directory")]
    public class Directory
    {
        public Directory()
        {
            this.File = new HashSet<File>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }     
  
        public long ParentId { get; set; }

        [Required]
        [Display(Name = "Directory Name")]
        public string Name { get; set; }
        public string Path { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<File> File { get; set; }
        

        public static Directory CreateModel(DirectoryViewModel directory)
        {
            return new Directory()
            {
                Id = directory.Id,
                CreatedAt = directory.CreatedAt,
                ModifiedAt = directory.ModifiedAt,
                Name = directory.Name,
                ParentId = directory.ParentId,                
            };
        }
    }
}