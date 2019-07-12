using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Demo0607.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://www.carlsontoylibrary.co.nz/home/about-us")));
        }

        public ICommand OpenWebCommand { get; }
    }
}