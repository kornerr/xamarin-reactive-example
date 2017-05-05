using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class LoginPage
    {
        public LoginPage(LoginVM loginVM)
        {
            InitializeComponent();

            ViewModel = loginVM;
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
            // Display spinner while loading.
            this.WhenAnyValue(x => x.ViewModel.IsLoading)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy =>
                    {
                        Username.IsEnabled = !busy;
                        Password.IsEnabled = !busy;
                        Processing.IsVisible = busy;
                        LoginView.IsVisible = !busy;
                    });

            // Observe animated button press.
            Debug.WriteLine("AnimatedButton: '{0}'", AnimatedButton);
            var obs = 
                Observable.FromEventPattern(
                    ev => AnimatedButton.ButtonClicked += ev,
                    ev => AnimatedButton.ButtonClicked -= ev);
            obs.Subscribe(
                _ => {
                    Debug.WriteLine("LoginPage. AnimatedButton clicked");
                    LoginView.LayoutTo(Target.Bounds, 500, Easing.CubicOut);
                    Debug.WriteLine(
                        "LoginPage. OnClick. Target.Bounds: '{0}x{1},{2}x{3}'",
                        Target.Bounds.X,
                        Target.Bounds.Y,
                        Target.Bounds.Width,
                        Target.Bounds.Height
                    );
                });
            Debug.WriteLine("LoginPage. Constructed");

			var layoutChangeObservable =
				Observable.FromEventPattern(
					ev => this.LayoutChanged += ev,
					ev => this.LayoutChanged -= ev);
			var appearingObservable =
				Observable.FromEventPattern(
					ev => this.Appearing += ev,
					ev => this.Appearing -= ev);
			var layoutAndAppearingObservable =
				Observable.CombineLatest(
					appearingObservable,
					layoutChangeObservable).Finally(() => { Debug.WriteLine(@"Appearing or LayoutChanged"); });
				          

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("LoginPage. OnAppearing");
            // Show LoginView upon appearing.
            //LoginView.LayoutTo(Target.Bounds, 500, Easing.CubicOut);
            Debug.WriteLine(
                "LoginPage. OnAppearing. Target.Bounds: '{0}x{1},{2}x{3}'",
                Target.Bounds.X,
                Target.Bounds.Y,
                Target.Bounds.Width,
                Target.Bounds.Height
            );
        }
    }
}

