using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystems.Repository
{
    public class WebRepository: IWebRepository
    {
        public void Set<T>(T value)
        {
            var key = typeof(T).FullName;
            HttpContext.Current.Session[key] = value;
        }

        public void Set<T>(T value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }

        public T Get<T>()
        {
            var key = typeof(T).FullName;

            return GetKeyValue<T>(key);
        }

        public T Get<T>(string key)
        {
            return GetKeyValue<T>(key);
        }

        private static T GetKeyValue<T>(string key)
        {
            object retVal = HttpContext.Current.Session[key];
            if (retVal is T)
                return (T)retVal;
            else if (retVal == null)
                return default(T);
            else
                throw new Exception("Object returned was of the wrong type.");
            throw new NotImplementedException();
        }

        public void Remove<T>()
        {
            var key = typeof(T).FullName;
            if (HttpContext.Current != null && HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
    }
}