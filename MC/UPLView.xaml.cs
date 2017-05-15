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
		//public IObservable<String> UsernameObservable;

        public UPLView ()
        {
            InitializeComponent();

            setupUsernamePassword();
            Debug.WriteLine("UPLView()");
        }

        private void setupUsernamePassword()
        {
            var usernameObservable =
                Observable.FromEventPattern(
                    ev => Username.TextChanged += ev,
                    ev => Username.TextChanged -= ev);
        }
    }
}

