using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystems.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystems.Repository;
namespace FileSystem.Tests
{
    [TestClass()]
    public class FileControllerTests
    {
        private readonly FilesController _controller;
        public FileControllerTests()
        {
            _controller = new FilesController(
                            new FileRepository(), new WebRepository());
        }

        [TestMethod()]
        public void IndexTest()
        {
           //Write File Controller Tests
        }

        [TestMethod()]
        public void EditTest()
        {
            //Write File Controller Tests
        }


        [TestMethod()]
        public void DeleteTest()
        {
            //Write File Controller Tests
        }

        [TestMethod()]
        public void DownloadsTest()
        {
            //Write File Controller Tests
        }
    }
}
