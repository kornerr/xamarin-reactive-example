using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class UPLView : ContentView
    {
        public IObservable<System.Reactive.EventPattern<EventArgs>> UsernameObservable;
        public IObservable<System.Reactive.EventPattern<EventArgs>> PasswordObservable;

        public UPLView ()
        {
            InitializeComponent();

            setupUsernamePassword();
            setupLogin();
            Debug.WriteLine("UPLView()");
        }

        private void setupLogin()
        {
            Observable.Merge(
                UsernameObservable,
                PasswordObservable
            ).StartWith("abc").
                      Subscribe(_ =>
            {
                bool isUsernameValid =
                    !String.IsNullOrWhiteSpace(Username.Text);
                bool isPasswordValid =
                    !String.IsNullOrWhiteSpace(Password.Text);
                Login.IsEnabled =
                    isUsernameValid &&
                    isPasswordValid;
                Debug.WriteLine("Username/password changed");
            });
        }
        private void setupUsernamePassword()
        {
            UsernameObservable =
                Observable.FromEventPattern<EventArgs>(Username, "TextChanged");
            PasswordObservable =
                Observable.FromEventPattern<EventArgs>(Password, "TextChanged");
        }
    }
}

