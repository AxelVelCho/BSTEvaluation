using BSTCodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;

namespace BSTCodeChallenge.Controllers
{
    public class CacheController : Controller
    {
        public static IMemoryCache memoryCache;
        public CacheController()
        {
            memoryCache = new MemoryCache(new MemoryCacheOptions());
            InitializeAsync();
        }

        public static async Task InitializeAsync()
        {
            string urlCategory = "https://retoolapi.dev/TglAXS/category";
            string urlSuppliers = "https://retoolapi.dev/4ZIYMe/suppliers";

            HttpClient client = new HttpClient();

            string responseCategories = await client.GetStringAsync(urlCategory);
            string responseSuppliers = await client.GetStringAsync(urlSuppliers);

            var suppliers = JsonConvert.DeserializeObject<List<SupplierEntity>>(responseSuppliers);
            var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(responseCategories);
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            memoryCache.Set("Suppliers", suppliers, cacheEntryOptions);
            memoryCache.Set("Categories", categories, cacheEntryOptions);
        }

        public static List<SupplierEntity> getSuppliers()
        {
            return memoryCache.Get<List<SupplierEntity>>("Suppliers");
        }

        public static List<CategoryEntity> getCategory()
        {
            return memoryCache.Get<List<CategoryEntity>>("Categories");
        }
    }
}
