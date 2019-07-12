using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Demo0607.Models;

[assembly: Xamarin.Forms.Dependency(typeof(Demo0607.Services.MockDataStore))]
namespace Demo0607.Services
{
    public class MockDataStore : IDataStore<Toy>
    {
        List<Toy> items;

        public MockDataStore()
        {
            items = new List<Toy>();
            var mockItems = new List<Toy>
            {
                new Toy { Id = "G12", Name = "Plane", Description="This is an item description..........", Age = "2", Image = "sp01.png", Pieces = "24", State = "1" },
                new Toy { Id = "K133", Name = "Heeeeeeelicop", Description="This is an item description.", Age = "0-3", Image = "sp02.png", Pieces = "12", State = "1" },
                new Toy { Id = "D61", Name = "Jet", Description="This is an item description.", Age = "3", Image = "sp03.png", Pieces = "9", State = "1"  },
                new Toy { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Description="This is an item description." },
                new Toy { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Description="This is an item description." },
                new Toy { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Toy item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Toy item)
        {
            var _item = items.Where((Toy arg) => arg.Id == item.Id).FirstOrDefault();
            var _index = items.IndexOf(_item);
            items.Remove(_item);
            items.Insert(_index, item);
            //items.Remove(_item);
            //items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Toy arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Toy> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Toy>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}