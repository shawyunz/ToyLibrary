using Demo0607.Models;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Demo0607.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        User user = new User();
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        public ICommand CmdLogin { get; set; }

        public UserViewModel()
        {
            Title = "About";

            CmdLogin = new Command(OnLogin);
        }

        public void OnLogin()
        {
            if (User.user_id < 1)
            {
                MessagingCenter.Send(this, "LoginAlert", User);
            }
            else
            {
                //success
                //set user to this.User
                MessagingCenter.Send(this, "LoginSucc");
            }
        }
    }
}