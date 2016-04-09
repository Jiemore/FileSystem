using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using FileSystems.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystems.Repository;
namespace FileSystem.Tests
{
    [TestClass()]
    public class DirectoryTests
    {
        private readonly DirectoryController _controller;
        public DirectoryTests()
        {
            _controller = new DirectoryController(
                            new DirectoryRepository(), new WebRepository());
        }
            
        [TestMethod()]
        public void IndexTest()
        {
            var item = _controller.Index();
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void IndexService()
        {
            var data = new DirectoryRepository().GetDirectories();
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count() > 0);
            Assert.IsTrue(data.ToList().First().Name == "$root");
        }

        [TestMethod()]
        public void CreateTest()
        {
            var item = _controller.Create(11);
            Assert.IsNotNull(item);

        }

        [TestMethod()]
        public void EditTest()
        {
            var item = _controller.Edit();
            Assert.IsNotNull(item);

        }


    }
}
