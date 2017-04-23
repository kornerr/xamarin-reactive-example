
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
            _loginVM = new LoginVM();
            _loginPage = new LoginPage(_loginVM);

            MainPage = _loginPage;

            //setupGitHubResources();
            //setupGitHubLoading();

            setupMGR();
            setupMGRLoading();
        }

        public void setupGitHubResources()
        {
            _client = new GitHubClient();
            _ghr = new GitHubResources(_client);

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
        }

        void setupGitHubLoading()
        {
            // Peform request.
            this.WhenAnyValue(x => x._loginVM.IsLogging)
			    .Where(x => x == true)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(executing =>
                    {
					    Debug.WriteLine("AppCoordinator. Refresh");
                        _ghr.refresh();
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

        public void setupMGR()
        {
            _mgrClient = new MGRClient();
            _mgr = new MGR(_mgrClient);

            // Print result of the request.
            this.WhenAnyValue(x => x._mgr.Auth)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(auth =>
                    {
                        Debug.WriteLine(
                            "AppCoordinator. Authorize(access token: '{0}' refresh token: '{1}')",
                            auth.accessToken,
                            auth.refreshToken);
                    });
        }

        void setupMGRLoading()
        {
            // Peform request.
            this.WhenAnyValue(x => x._loginVM.IsLogging)
			    .Where(x => x == true)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(executing =>
                    {
					    Debug.WriteLine("AppCoordinator. Authorize");
                        _mgr.authorize(_loginVM.Username, _loginVM.Password);
                    });

            // Display spinner while request is in progress.
            this.WhenAnyValue(x => x._mgr.AuthStatus)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. MGR.AuthStatus: '{0}'", status);
                        _loginVM.IsLoading = (status == ModelRequestStatus.Process);
                    });
        }

        private GitHubClient _client;
        private GitHubResources _ghr;

        private MGRClient _mgrClient;
        private MGR _mgr;

        private LoginVM _loginVM;
        private LoginPage _loginPage;
    }
}

