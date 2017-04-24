using ReactiveUI;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using System;

namespace MC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            _coordinator = new AppCoordinator();

            MainPage = _coordinator.RootPage;
            // Monitor main page change.
			this.WhenAnyValue(x => x._coordinator.RootPage)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(page =>
                    {
					    Debug.WriteLine("App. Assign MainPage: '{0}'", page);
						MainPage = page;
                    });
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

        private AppCoordinator _coordinator;
    }
}

