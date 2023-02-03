using News.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {   new Item {Id = Guid.NewGuid().ToString(),Text="business", Description="Business"},
                new Item {Id = Guid.NewGuid().ToString(),Text="entertainment", Description="Entertainment"},
                new Item {Id = Guid.NewGuid().ToString(),Text="general", Description="General"},
                new Item {Id = Guid.NewGuid().ToString(),Text="health", Description="Health"},
                new Item {Id = Guid.NewGuid().ToString(),Text="science", Description="Science"},
                new Item {Id = Guid.NewGuid().ToString(),Text="sports", Description="Sports"},
                new Item {Id = Guid.NewGuid().ToString(),Text="technology", Description="Technology"}
            };     
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}