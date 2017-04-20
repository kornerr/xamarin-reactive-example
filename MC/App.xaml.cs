using ReactiveUI;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using System;

namespace MC
{
    public partial class App : Application
    {
        public LoginVM loginVM;
        public LoginPage loginPage;

        public App()
        {
            InitializeComponent();

            loginVM = new LoginVM();
            loginPage = new LoginPage(loginVM);

            loginPage
                .WhenAnyValue(x => x.ViewModel.IsLoading)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy =>
                {
                    Debug.WriteLine("App. LoginPage. WhenAnyValue" + busy);
                });

            this.WhenAnyValue(x => x.loginVM.IsLoading)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy =>
                {
                    Debug.WriteLine("App. LoginVM.IsLoading : {0}", busy);
                });
            this.WhenAnyValue(x => x.loginVM.Login.IsExecuting)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(executing =>
                {
                    Debug.WriteLine("App. LoginPage.Login.IsExecuting : {0}", executing);
                });
			this.WhenAnyValue(x => x.loginPage.signal)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(signal =>
			    {
					Debug.WriteLine("App. LoginPage.Signal : {0}", signal);
			    });

            MainPage = loginPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
