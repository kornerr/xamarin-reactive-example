using ReactiveUI;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MC
{
	public partial class LoginPage : ContentPage, IViewFor<LoginVM>
	{
		public LoginPage()
		{
			InitializeComponent();

            ViewModel = new LoginVM();

            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
		}

        // Boilerplate code.

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create(
                nameof(ViewModel),
                typeof(LoginVM),
                typeof(LoginPage),
                null,
                BindingMode.OneWay);
        public LoginVM ViewModel
        {
            get { return (LoginVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginVM)value; }
        }
	}
}

