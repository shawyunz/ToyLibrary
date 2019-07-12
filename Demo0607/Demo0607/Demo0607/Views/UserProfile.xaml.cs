using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo0607.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
		public UserProfile ()
		{
			InitializeComponent ();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //Application.MainPage = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //not login
            //if (viewModel.Lists.Count == 0)
            //Navigation.PushAsync(new UserLogin());
        }

        async void Click_Logout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLogin());
        }
        async void Click_About(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}