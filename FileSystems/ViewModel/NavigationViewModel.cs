using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystems.ViewModel
{
    public class NavigationViewModel
    {
        public long id { get; set; }
        public string Name { get; set; }
    }

    public class NavigationListViewModel : Dictionary<long, NavigationViewModel>
    {

    }
}