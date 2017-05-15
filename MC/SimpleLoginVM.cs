using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
    public class SimpleLoginVM : ReactiveObject
    {
        // Username.
        string _username;
        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }

        // Password.
        string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public SimpleLoginVM()
        {
        }
    }
}

