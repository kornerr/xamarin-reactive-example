using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public enum LoginPageSignal
    {
        None,
		Login,
        Logout
    };

    public partial class LoginPage : ContentPage, IViewFor<LoginVM>
    {
        public LoginPageSignal signal;

        public LoginPage(LoginVM loginVM)
        {
            InitializeComponent();

            ViewModel = loginVM;
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
            this.WhenAnyValue(x => x.ViewModel.IsLoading)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy =>
                    {
                        Username.IsEnabled = !busy;
                        Password.IsEnabled = !busy;
                        Processing.IsVisible = busy;
                        Main.IsVisible = !busy;
                    });
			Login.Events().Clicked.Subscribe(_ =>
			{
				signal = LoginPageSignal.Login;
				Debug.WriteLine("LoginPage. Set signal");
			});
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

