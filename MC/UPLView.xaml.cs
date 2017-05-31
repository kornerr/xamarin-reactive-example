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
        public UPLView ()
        {
            InitializeComponent();

            setupLogin();
            Debug.WriteLine("UPLView()");
        }

        private void setupLogin()
        {
            Observable.Merge(
				Username.Events().TextChanged,
				Password.Events().TextChanged
            ).Select(_ => Unit.Default)
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
                Debug.WriteLine("Username/password changed");
            });
        }
    }
}

