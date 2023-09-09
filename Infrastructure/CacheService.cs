using AspNETInMemoryCache.Services;
using System.Runtime.Caching;

namespace AspNETInMemoryCache.Infrastructure
{
    public class CacheService : ICacheService
    {
        readonly ObjectCache _memoryCache = MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try { 
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object RemoveData(string key)
        {
            var res = true;

            try
            {

                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Remove(key);
                }
                else
                {
                    res = false;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset Expirationtime)
        {
            var res = true;

            try {

                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, Expirationtime);
                }
                else {
                    res = false;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
