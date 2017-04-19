using ReactiveUI;
using System;
using Xamarin.Forms;

namespace MC
{
	public class LoginVM : ReactiveObject
	{

        public LoginVM()
        {

        }

        public ReactiveCommand Login { get; protected set; }

        // Read-write property.
        string _username;
        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }

        // Read-write property.
        string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        // Read only property.
        readonly ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading
        {
            get { return _isLoading.Value; }
        }
	}
}

