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
            Debug.WriteLine("SimpleLoginPage. UPLView: '%p'", UPLView);

            Observable
                .FromEventPattern<UPLView.UPLViewSignalEventArgs>(
                    ev => UPLView.UPLViewSignalEvent += ev,
                    ev => UPLView.UPLViewSignalEvent -= ev)
                .Subscribe(
                    x => {
                        Debug.WriteLine("SimpleLoginPage. Event: '{0}'", x.EventArgs.signal);
                    });
        }
    }
}
