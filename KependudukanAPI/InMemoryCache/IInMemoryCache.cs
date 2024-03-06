using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace KependudukanAPI.InMemoryCache
{
    public interface IInMemoryCache
    {
        public List<dynamic> InMemoryCacheChecker(string key);
        public void AddInMemoryCache(string key, List<dynamic> list, CacheItemPriority priority);
        public void RemoveInMemoryCache(string key);
    }
}
