using Demo0607.Models;
using Demo0607.ViewModels;
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
	public partial class UserLogin : ContentPage
	{
        public UserViewModel viewModel;

		public UserLogin ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new UserViewModel();

            #region message
            MessagingCenter.Subscribe<UserViewModel, User>(this, "LoginAlert", (sender, user) =>
            {
                DisplayAlert("Title", user.user_id + "", "okkk");
            });

            MessagingCenter.Subscribe<UserViewModel>(this, "LoginSucc", async (sender) =>
            {
                await Navigation.PopAsync();
            });
            #endregion

            entry_uid.Completed += (object sender, EventArgs e) =>
            {
                entry_pwd.Focus();
            };

            entry_pwd.Completed += (object sender, EventArgs e) =>
            {
                viewModel.CmdLogin.Execute(null);
            };
        }

        async void Click_Page_Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegister());
        }


    }
}