using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Demo0607.Models;
using Demo0607.Views;
using System.Linq;

namespace Demo0607.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Toy> ToyListing { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Toy Library";
            ToyListing = new ObservableCollection<Toy>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemAddPage, Toy>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Toy;
                ToyListing.Add(_item);
                await DataStore.AddItemAsync(_item);
            });

            MessagingCenter.Subscribe<ItemEditPage, Toy>(this, "UpdateItem", async (obj, item) =>
            {
                var _newitem = item as Toy;
                var _item = ToyListing.Where((Toy arg) => arg.Id == item.Id).FirstOrDefault();
                var _index = ToyListing.IndexOf(_item);
                ToyListing.Remove(_item);
                ToyListing.Add(_newitem);

                await DataStore.UpdateItemAsync(_newitem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ToyListing.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    ToyListing.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}