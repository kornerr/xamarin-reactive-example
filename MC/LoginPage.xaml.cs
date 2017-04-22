using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
	/*
    public enum LoginPageSignal
    {
        None,
        Login,
        Logout
    };
    */

    public partial class LoginPage
    {
		/*
        LoginPageSignal _signal;
        public LoginPageSignal Signal
        {
            get { return _signal; }
            set { this.RaiseAndSetIfChanged(ref _signal, value); }
        }
        */

        public LoginPage(LoginVM loginVM, GitHubResources ghr)
        {
            InitializeComponent();

            GHR = ghr;
            //this.BindCommand(ghr, vm => vm.Login, v => v.Login);

            ViewModel = loginVM;
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            /*
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
            */       
            Login.Events().Clicked.Subscribe(_ =>
            {
                Debug.WriteLine("LoginPage. Click. 01");
                GHR.refresh();
                Debug.WriteLine("LoginPage. Click. 02");
            });
            this.WhenAnyValue(x => x.GHR.GHRModel)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(ghrModel =>
                    {
                        Debug.WriteLine(
                            "WHEN_ANY.GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                            ghrModel.current_user_url,
                            ghrModel.hub_url);
                    });
        }

        protected GitHubResources GHR;

    }


}

