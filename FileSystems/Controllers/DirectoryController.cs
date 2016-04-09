using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystems.Models;
using FileSystems.DAL;
using FileSystems.Repository;
using FileSystems.ViewModel;

namespace FileSystems.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IWebRepository _webrepository;
       
        
        public DirectoryController(IDirectoryRepository directoryReposit, 
                    IWebRepository webrepository)
        {
            _directoryRepository = directoryReposit;
            _webrepository = webrepository;
        }
        
        public ActionResult Index()
        {
            _webrepository.Remove<NavigationListViewModel>();
            var model = GetDirectory().Where(x => x.ParentId.Equals(0)).ToList();         
            var viewModel = DirectoryViewModel.CreateViewModelList(model);           
            return View(viewModel);
        }
        //
        // GET: /Directory/Details/5
        public ActionResult Details(long id = 0)
        {
            var data = GetDirectory();
            var detailsViewModel = DirectoryDetailsViewModel.CreateViewModel(data, id);
            SetNavigationMenu(id, data, detailsViewModel);        
            return View(detailsViewModel);
        }

        //
        // GET: /Directory/Create
        public ActionResult Create(long parentId)
        {
            var viewModel = new DirectoryViewModel()
            {
                CreatedAt = DateTime.Now,
                ParentId = parentId,
                
            };
            return View(viewModel);
        }

        //
        // POST: /Directory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DirectoryViewModel directory)
        {
            var model = Directory.CreateModel(directory);
            _directoryRepository.CreateDirectory(model);
            return ReturnToDetails(directory.ParentId);
        }

        //
        // GET: /Directory/Edit/5
        public ActionResult Edit(long id = 0)
        {
            var directoryVM = DirectoryViewModel.CreateViewModel((GetDirectory())
                                                    .Find(x => x.Id == id));
            if (directoryVM == null)
            {
                return HttpNotFound();
            }
            return View(directoryVM);
        }

        //
        // POST: /Directory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DirectoryViewModel directory)
        {
            _directoryRepository.UpdateDirectory(Directory.CreateModel(directory));
            return ReturnToDetails(directory.ParentId);
        }

        //
        // GET: /Directory/Delete/5
        public ActionResult Delete(long parentId, long id)
        {
            var directoryVM = DirectoryViewModel.CreateViewModel((GetDirectory()).Find(x => x.Id == id));
            if (directoryVM == null)
            {
                return HttpNotFound();
            }
            _directoryRepository.DeleteDirectory(id);
            return ReturnToDetails(parentId);
        }

        public ActionResult MenuNavigation(long id)
        {
            var navList = _webrepository.Get<NavigationListViewModel>();
            var newNavList = new NavigationListViewModel();

            foreach(var item in navList)
            {
                if(item.Key == id)
                    break;
                newNavList.Add(item.Key, item.Value);
            }

            _webrepository.Set(newNavList);

            return RedirectToAction("Details", new { id = id });
        }


        #region PrivateMethods
        private ActionResult ReturnToDetails(long parentId)
        {
            return RedirectToAction("Details", new { id = parentId });
        }
        
        private List<Directory> GetDirectory()
        {
            return _directoryRepository.GetDirectories().ToList(); ;
        }

        private void SetNavigationMenu(long id, List<Directory> data, DirectoryDetailsViewModel detailsViewModel)
        {
            var menuItems = _webrepository.Get<NavigationListViewModel>();
            if (menuItems == null)
            {
                _webrepository.Set(detailsViewModel.NavigationMenu);
            }
            else
            {
                var navList = _webrepository.Get<NavigationListViewModel>();
                var navItem = detailsViewModel.NavigationMenu.First();

                if (!navList.ContainsKey(navItem.Key))
                {
                    navList.Add(navItem.Key, navItem.Value);
                }

                detailsViewModel.NavigationMenu = navList;

            }
            _webrepository.Set<Directory>(data.Find(x => x.Id == id));
        }

        #endregion

    }
}