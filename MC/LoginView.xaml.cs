using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class LoginView
    {
        public LoginView(LoginViewVM viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
        }
    }

    public class LoginViewVM : ReactiveObject
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

        public LoginViewVM()
        {
        }
    }
}

