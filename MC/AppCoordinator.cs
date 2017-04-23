
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{

    public class AppCoordinator : ReactiveObject
    {
        public ContentPage MainPage;

        public AppCoordinator()
        {
            _client = new GitHubClient();
            _ghr = new GitHubResources(_client);

            _loginVM = new LoginVM();
            _loginPage = new LoginPage(_loginVM);

            MainPage = _loginPage;

            // Peform request.
			this.WhenAnyValue(x => x._loginVM.IsLogging)
			    .Where(x => x == true)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(executing =>
                    {
					    Debug.WriteLine("AppCoordinator. Refresh");
                        _ghr.refresh();
                    });

            // Print result of the request.
            this.WhenAnyValue(x => x._ghr.GHRModel)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(ghrModel =>
                    {
                        Debug.WriteLine(
                            "AppCoordinator. GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                            ghrModel.current_user_url,
                            ghrModel.hub_url);
                    });

            // Display spinner while request is in progress.
            this.WhenAnyValue(x => x._ghr.RefreshStatus)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. GHR.RefreshStatus: '{0}'", status);
                        _loginVM.IsLoading = (status == ModelRequestStatus.Process);
                    });
        }

        private GitHubClient _client;
        private GitHubResources _ghr;
        private LoginVM _loginVM;
        private LoginPage _loginPage;
    }
}

