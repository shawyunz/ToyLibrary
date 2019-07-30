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

            InitLoginPage();

        }
        public UserLogin(UserViewModel vm)
        {
            InitializeComponent();

            BindingContext = viewModel = vm;

            InitLoginPage();
        }

        void InitLoginPage()
        {
            #region message
            MessagingCenter.Subscribe<UserViewModel, User>(this, "LoginAlert", async (sender, user) =>
            {
                await DisplayAlert("Login", "Login failed, please try again!", "OK");
                //Navigation.InsertPageBefore(new UserProfile(viewModel), this);
                //await Navigation.PopAsync();
            });

            MessagingCenter.Subscribe<UserViewModel>(this, "LoginSucc", async (sender) =>
            {
                await DisplayAlert("Login", "Welcome to Toy Library, " + viewModel.User.last_name + "!!", "OK");
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