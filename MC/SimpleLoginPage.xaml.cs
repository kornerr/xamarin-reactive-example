using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class SimpleLoginPage
    {
        public SimpleLoginPage(SimpleLoginVM viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            // Listen to UPLView signals.
            this.WhenAnyValue(x => x.UPLView.VM.Signal)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(signal =>
                {
                    Debug.WriteLine("SimpleLoginPage. Signal: '{0}'", signal);
                });
            // Listen to UPLView username change.
            this.WhenAnyValue(x => x.UPLView.VM.Username)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(username =>
                {
                    Debug.WriteLine("SimpleLoginPage. Username: '{0}'", username);
                });
        }
    }
}

