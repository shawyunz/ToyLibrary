using Demo0607.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo0607.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserRegister : ContentPage
    {
        CloudDBHelper dbhelper = new CloudDBHelper();

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
            string UID = txtid.Text;
            string UserName = txtemail.Text;
            string Password = txtpwd.Text;
            string ConfirmPassword = txtpwd2.Text;

            //strong password check
            string patdi = @"\d+"; //match digits
            string patupp = @"[A-Z]+"; //match upper cases
            string patlow = @"[a-z]+"; //match lower cases
            //string patsym = @"[`~!@$%^&\\-\\+*/_=,;.':|\\(\\)\\[\\]\\{\\}]+"; //match symbols
            string patsym = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"; //match symbols
            Match id = Regex.Match(Password, patdi);
            Match upp = Regex.Match(Password, patupp);
            Match low = Regex.Match(Password, patlow);
            Match sym = Regex.Match(Password, patsym);

            //validations for input
            if (Password != ConfirmPassword)
            {
                await DisplayAlert("Register", "Please type the same password to confirm!", "OK");
            }
            else if (string.IsNullOrEmpty(UID) || string.IsNullOrEmpty(UserName)
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await DisplayAlert("Register", "Please fill all fields to finish the registration!", "OK");
            }
            //else if (!new EmailAddressAttribute().IsValid(UID))
            //{
            //    showMessage("Please use a valid email address!");
            //}

            else if (Password.Length < 6)
            {
                await DisplayAlert("Register", "Please use a stronger password with at least 6 length!", "OK");
            }
            else if (!(id.Success && upp.Success && low.Success && sym.Success))
            {
                await DisplayAlert("Register", "Please use a stronger password!", "OK");
            }
            else
            {
                await dbhelper.AddPerson(Convert.ToInt32(txtid.Text), txtemail.Text, txtfname.Text, txtlname.Text, getHashed(txtpwd.Text, txtid.Text));
                await DisplayAlert("Success", "Person Added Successfully", "OK");
                await Navigation.PopAsync();
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