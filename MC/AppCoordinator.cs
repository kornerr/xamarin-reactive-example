
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{

    public class AppCoordinator : ReactiveObject
    {
        public ContentPage _mainPage;
        public ContentPage MainPage
        {
            get { return _mainPage; }
            protected set { this.RaiseAndSetIfChanged(ref _mainPage, value); }
        }

        public AppCoordinator()
        {
            _loginVM = new LoginVM();
            _loginPage = new LoginPage(_loginVM);

            _successPage = new SuccessPage();

            MainPage = _loginPage;

            //setupGitHubResources();
            //setupGitHubLoading();

            setupMGR();
            setupMGRAuth();
            setupMGRAuthTransitions();
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

        void setupMGRAuth()
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
        void setupMGRAuthTransitions()
        {
            // Go to 'Success' upon successful authorization.
            this.WhenAnyValue(x => x._mgr.AuthStatus)
                .Where(x => x == ModelRequestStatus.Success)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. set main page to SuccessPage");
                        MainPage = _successPage;
                    });

            // Go to 'Failure' upon failed authorization.
            this.WhenAnyValue(x => x._mgr.AuthStatus)
                .Where(x => x == ModelRequestStatus.Failure)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. set main page to FailurePage");
                        //MainPage = _failurePage;
                    });
        }

        private GitHubClient _client;
        private GitHubResources _ghr;

        private MGRClient _mgrClient;
        private MGR _mgr;

        private LoginVM _loginVM;
        private LoginPage _loginPage;

        private SuccessVM _successVM;
        private SuccessPage _successPage;
    }
}

