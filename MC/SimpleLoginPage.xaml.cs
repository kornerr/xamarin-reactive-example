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
        }
    }
}
