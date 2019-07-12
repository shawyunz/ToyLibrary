using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo0607.Models;
using Demo0607.ViewModels;

namespace Demo0607.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEditPage(viewModel));
            Navigation.RemovePage(this);
        }
    }
}