using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    // UPLView signals.
    public enum UPLViewSignal
    {
        None,
        Login,
        Credentials
    };

    public partial class UPLView : ContentView
    {
        // ViewModel reports necessary property changes.
        public class ViewModel : ReactiveObject
        {
            // Signal.
            UPLViewSignal _signal;
            public UPLViewSignal Signal
            {
                get { return _signal; }
                set { this.RaiseAndSetIfChanged(ref _signal, value); }
            }

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
        };

        public ViewModel VM;

        public UPLView ()
        {
            InitializeComponent();
            setupUPLView();
        }

        private void setupCredentials()
        {
            // Report username.
            Username.Events().TextChanged.Subscribe(x =>
            {
				VM.Username = x.NewTextValue;
            });
            // Accept username.
            this.WhenAnyValue(x => x.VM.Username)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(username =>
                {
                    if (Username.Text != username) {
                        Username.Text = username;
                    }
                });
            // Report password.
            Password.Events().TextChanged.Subscribe(x =>
            {
				VM.Password = x.NewTextValue;
            });
            // Accept password.
            this.WhenAnyValue(x => x.VM.Password)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(password =>
                {
                    if (Password.Text != password) {
                        Password.Text = password;
                    }
                });
            // Report change of credentials.
            Observable
                .Merge(
                    Username.Events().TextChanged,
                    Password.Events().TextChanged
                )
                .Select(_ => Unit.Default)
                .StartWith(Unit.Default)
                .Subscribe(_ =>
                {
				    VM.Signal = UPLViewSignal.Credentials;
                    VM.Signal = UPLViewSignal.None;
                });
        }
        private void setupLogin()
        {
            // Set Login state based on credentials validity.
            Observable
                .Merge(
                    Username.Events().TextChanged,
                    Password.Events().TextChanged
                )
                .Select(_ => Unit.Default)
                .StartWith(Unit.Default)
                .Subscribe(_ =>
                {
                    bool isUsernameValid =
                        !String.IsNullOrWhiteSpace(Username.Text);
                    bool isPasswordValid =
                        !String.IsNullOrWhiteSpace(Password.Text);
                    Login.IsEnabled =
                        isUsernameValid &&
                        isPasswordValid;
                });
            // Report login click.
            Login.Events().Clicked.Subscribe(_ =>
            {
				VM.Signal = UPLViewSignal.Login;
                VM.Signal = UPLViewSignal.None;
            });
        }
        private void setupUPLView()
        {
            VM = new ViewModel();
            setupLogin();
            setupCredentials();
        }
    }
}

