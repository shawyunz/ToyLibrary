using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo0607.Models;

namespace Demo0607.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemAddPage : ContentPage
    {
        public Toy Item { get; set; }

        public ItemAddPage()
        {
            InitializeComponent();

            Item = new Toy
            {
                Name = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}