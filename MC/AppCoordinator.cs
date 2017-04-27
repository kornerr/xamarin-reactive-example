
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{

    public class AppCoordinator : ReactiveObject
    {
        // MainPage.
        public ContentPage _rootPage;
        public ContentPage RootPage
        {
            get { return _rootPage; }
            protected set { this.RaiseAndSetIfChanged(ref _rootPage, value); }
        }

        public AppCoordinator()
        {
            _loginVM = new LoginVM();
            _loginPage = new LoginPage(_loginVM);

            _successPage = new SuccessPage();
            _failurePage = new FailurePage();

            //_rootPage = _loginPage;

            //_animationPage = new AnimationPage();
            _animatedLoginPage = new AnimatedLoginPage();
            _rootPage = _animatedLoginPage;

            setupMGR();
            setupMGRAuth();
            setupMGRAuthTransitions();
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
                        RootPage = _successPage;
                    });

            // Go to 'Failure' upon failed authorization.
            this.WhenAnyValue(x => x._mgr.AuthStatus)
                .Where(x => x == ModelRequestStatus.Failure)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. set main page to FailurePage");
                        RootPage = _failurePage;
                    });
        }

        private MGRClient _mgrClient;
        private MGR _mgr;

        private LoginVM _loginVM;
        private LoginPage _loginPage;

        private SuccessPage _successPage;
        private FailurePage _failurePage;

        private AnimationPage _animationPage;
        private AnimatedLoginPage _animatedLoginPage;
    }
}

