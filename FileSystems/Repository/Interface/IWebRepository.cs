using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystems.Repository
{
    public interface IWebRepository
    {
        void Set<T>(T value);
        void Set<T>(T value, string key);
        T Get<T>();
        T Get<T>(string key);
        void Remove<T>();
    }
}