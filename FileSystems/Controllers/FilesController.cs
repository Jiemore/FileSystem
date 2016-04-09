using FileSystems.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystems.Models;
using FileSystems.ViewModel;

namespace FileSystems.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileRepository _filesRepository;
        private readonly IWebRepository _webrepository;

        public FilesController(IFileRepository filesRepositor, IWebRepository webrepository)
        {
            _filesRepository = filesRepositor;
            _webrepository = webrepository;
        }

        public ActionResult Upload()
        {           
            return View(new FileViewModel());
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {   
            var directory = _webrepository.Get<Directory>();
            var directoryId = directory.Id;
            _filesRepository.UploadFiles(new Files(files), directoryId);
            return RedirectToAction("Details", "Directory", new { id = directoryId });
        }

        public ActionResult Edit(long id)
        {
            var file = _filesRepository.Getfile(id);
            var fileViewModel = FileViewModel.CreateViewModel(file);
            return View(fileViewModel);
        }
        //
        // POST: /Directory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FileViewModel fileViewModel)
        {
            var file = _filesRepository.Getfile(fileViewModel.Id);
            var fileModel = FileSystems.Models.File.CreateModel(fileViewModel);
            fileModel.UpdateFileOnServer(file.Name);
            _filesRepository.EditFiles(fileModel);
            return RedirectToAction("Details", "Directory", new { id = fileViewModel.ParentId });
        }       

        public ActionResult Delete(long directoryId, long fileId)
        {
            var file = _filesRepository.Getfile(fileId);
            file.DeleteFileFromServer();
            _filesRepository.DeleteFiles(fileId);
            return RedirectToAction("Details", "Directory", new { id = directoryId });
        }

        public FileResult Download(long id)
        {
            var file = _filesRepository.Getfile(id);
            var fileBytes = file.GetFileFromServer();            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
        }
    }
}
