using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo0607.Models;
using Demo0607.ViewModels;

namespace Demo0607.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemEditPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemEditPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            await Navigation.PopAsync();
        }
    }
}