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
    public partial class UPLView : ContentView
    {
        public enum Signal {
            None,
            Login,
            Credentials
        };
        public class SignalEventArgs : EventArgs
        {
            public Signal signal;
        };

        public event EventHandler<SignalEventArgs> SignalEvent;

        public UPLView ()
        {
            InitializeComponent();

            setupLogin();
        }

        private void setupLogin()
        {
            // Set Login state based on credentials validity.
            Observable
                .Merge(
                    Username.Events().TextChanged,
                    Password.Events().TextChanged)
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
            // Report change of credentials.
            Observable
                .Merge(
                    Username.Events().TextChanged,
                    Password.Events().TextChanged)
                .Select(_ => Unit.Default)
                .StartWith(Unit.Default)
                .Subscribe(_ =>
                {
                    Debug.WriteLine("UPLView. Username/password changed");
                    if (SignalEvent != null)
                    {
                        Debug.WriteLine("UPLView. Reporting Credentials event");
                        var ev = new SignalEventArgs();
                        ev.signal = Signal.Credentials;
                        SignalEvent(this, ev);
                    }
                });
            // Report login.
            Login.Events().Clicked.Subscribe(_ =>
            {
                Debug.WriteLine("UPLView. Login clicked");
                if (SignalEvent != null)
                {
                    Debug.WriteLine("UPLView. Reporting Login event");
                    var ev = new SignalEventArgs();
                    ev.signal = Signal.Login;
                    SignalEvent(this, ev);
                }
            });
        }
    }
}

