using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class UPLView : ContentView
    {
        public enum UPLViewSignal {
            UPLViewSignalNone,
            UPLViewSignalAuth,
            UPLViewSignalCred
        };
        public class UPLViewSignalEventArgs : EventArgs
        {
            public UPLViewSignal signal;
        };

        public event EventHandler<UPLViewSignalEventArgs> UPLViewSignalEvent;

        public UPLView ()
        {
            InitializeComponent();

            setupLogin();
            Debug.WriteLine("UPLView()");
        }

        private void setupLogin()
        {
            Observable.Merge(
                Username.Events().TextChanged,
                Password.Events().TextChanged
            ).Select(_ => Unit.Default)
             .StartWith(Unit.Default)
             .Subscribe(_ =>
            {
                bool isUsernameValid =
                    !String.IsNullOrWhiteSpace(Username.Text);
                bool isPasswordValid =
                    !String.IsNullOrWhiteSpace(Password.Text);
                Login.IsEnabled =
                    isUsernameValid &&
                    isPasswordValid;
                Debug.WriteLine("UPLView. Username/password changed");
                if (UPLViewSignalEvent != null)
                {
                    Debug.WriteLine("UPLView. Reporting Cred event");
					var ev = new UPLViewSignalEventArgs();
                    ev.signal = UPLViewSignal.UPLViewSignalCred;
                    UPLViewSignalEvent(this, ev);
                }
            });
			Login.Events().Clicked.Subscribe(_ =>
			{
				Debug.WriteLine("UPLView. Login clicked");
				if (UPLViewSignalEvent != null)
				{
					Debug.WriteLine("UPLView. Reporting Auth event");
					var ev = new UPLViewSignalEventArgs();
					ev.signal = UPLViewSignal.UPLViewSignalAuth;
					UPLViewSignalEvent(this, ev);
				}
			});
        }
    }
}

