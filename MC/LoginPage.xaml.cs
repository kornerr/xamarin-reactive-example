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
                });

            // Animate login view and image view to new positions right after appearing.
            var layoutChangeObservable =
                Observable.FromEventPattern(
                    ev => this.LayoutChanged += ev,
                    ev => this.LayoutChanged -= ev);
            var appearingObservable =
                Observable.FromEventPattern(
                    ev => this.Appearing += ev,
                    ev => this.Appearing -= ev);
            var layoutAndAppearingObservable =
                Observable.Merge(
                    appearingObservable,
                    layoutChangeObservable
                ).Subscribe(
                    _ => {
                        Debug.WriteLine(
                            "LoginPage. Layout/Appear. Target.Bounds: '{0}x{1},{2}x{3}'",
                            Target.Bounds.X,
                            Target.Bounds.Y,
                            Target.Bounds.Width,
                            Target.Bounds.Height
                        );
                        // Only if bounds are valid.
                        if (Target.Bounds.Width != -1) {
                            Debug.WriteLine("LoginPage. Layout to Target.Bounds");
                            LoginView.LayoutTo(Target.Bounds, 500, Easing.CubicOut);
                            Splash.LayoutTo(TargetImage.Bounds, 500, Easing.CubicOut);
                        }
                    });
        }
    }
}

