using FileSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystems.ViewModel
{
    public class DirectoryDetailsViewModel
    {
        public DirectoryDetailsViewModel()
        {
            ChildrenDirectory = new List<DirectoryViewModel>();
            Files = new List<FileViewModel>();
            NavigationMenu = new NavigationListViewModel();
        }

        public long ParentId { get; set; }
        public long CurrentDirectoryId { get; set; }
        public NavigationListViewModel NavigationMenu { get; set; }
        public List<DirectoryViewModel> ChildrenDirectory { get; set; }
        public List<FileViewModel> Files { get; set; }

        public static DirectoryDetailsViewModel CreateViewModel(List<Directory> list, long id = 0)
        {
            var detailsViewModel = new DirectoryDetailsViewModel();
            var currentDirectory = list.Find(x => x.Id == id);
            var currentDirectoryChildren = list.FindAll(x => x.ParentId == id);
            detailsViewModel.ParentId = id;
            detailsViewModel.CurrentDirectoryId = id;
            detailsViewModel.ChildrenDirectory.AddRange(DirectoryViewModel.CreateViewModelList(currentDirectoryChildren));
            detailsViewModel.Files.AddRange(FileViewModel.CreateListViewModel(currentDirectory.File));
            detailsViewModel.NavigationMenu.Add(currentDirectory.Id, new NavigationViewModel()
            {
                id = currentDirectory.Id,
                Name = currentDirectory.Name
            });

            
            return detailsViewModel;
           
        }
    }

}