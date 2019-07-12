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
	public partial class UserRegister : ContentPage
	{
		public UserRegister ()
		{
			InitializeComponent ();
        }
        async void Click_Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void Click_Register(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}