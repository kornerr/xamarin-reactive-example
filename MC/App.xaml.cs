using ReactiveUI;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using System;

namespace MC
{
    public partial class App : Application
    {
        public GitHubClient client;
        public GitHubResources ghr;

        public LoginVM loginVM;
        public LoginPage loginPage;

        public App()
        {
            InitializeComponent();

            client = new GitHubClient();
            ghr = new GitHubResources(client);

            loginVM = new LoginVM();
            loginPage = new LoginPage(loginVM);

            // Peform request.
			this.WhenAnyValue(x => x.loginVM.IsLogging)
			    .Where(x => x == true)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(executing =>
                    {
					    Debug.WriteLine("App. Refresh");
                        ghr.refresh();
                    });

            // Print result of the request.
            this.WhenAnyValue(x => x.ghr.GHRModel)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(ghrModel =>
                    {
                        Debug.WriteLine(
                            "WHEN_ANY.GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                            ghrModel.current_user_url,
                            ghrModel.hub_url);
                    });

            // Display spinner while request is in progress.
            this.WhenAnyValue(x => x.ghr.RefreshStatus)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("GHR.RefreshStatus: '{0}'", status);
                        loginVM.IsLoading = (status == ModelRequestStatus.Process);
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
