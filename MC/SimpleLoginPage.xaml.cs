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

            Observable
                .FromEventPattern<UPLView.SignalEventArgs>(
                    ev => UPLView.SignalEvent += ev,
                    ev => UPLView.SignalEvent -= ev)
                .Subscribe(
                    x => {
                        Debug.WriteLine("SimpleLoginPage. UPLView signal: '{0}'", x.EventArgs.signal);
                    });
        }
    }
}

