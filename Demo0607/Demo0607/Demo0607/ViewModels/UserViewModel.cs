using Demo0607.Models;
using Demo0607.Services;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace Demo0607.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        User user = new User();
        CloudDBHelper dbhelper = new CloudDBHelper();
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        public ICommand CmdLogin { get; set; }

        public UserViewModel()
        {
            Title = "About";

            CmdLogin = new Command(async () => await ExecuteLogin());
        }

        async Task ExecuteLogin()
        {
            //test
            //if password = dbhelper.getUser(id).password

            var dbuser = await dbhelper.GetPerson(User.user_id);
            if (dbuser.password.Equals(getHashed(User.password, User.user_id.ToString())))
            {
                MessagingCenter.Send(this, "LoginSucc");
            }
            else
            {
                MessagingCenter.Send(this, "LoginAlert", User);
            }
        }
        
        private string getHashed(string str, string strSalt)
        {
            byte[] strByte = ASCIIEncoding.ASCII.GetBytes(str + strSalt + "AIS");
            byte[] hashByte = SHA256.Create().ComputeHash(strByte);
            return Convert.ToBase64String(hashByte);
        }

    }
}