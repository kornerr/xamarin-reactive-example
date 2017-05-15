using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace MC
{
    public partial class UPLView : ContentView
    {
        public event EventHandler UsernameChanged;

        public UPLView ()
        {
            InitializeComponent();

            setupUsername();
            //setupPassword();
            //setupLogin();
            Debug.WriteLine("UPLView()");
        }

        private void setupUsername()
        {
            Debug.WriteLine("UPLView.setupUsername");
            /*
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += async (s, e) =>
            {
                await this.ScaleTo(0.95, 50, Easing.CubicOut);
                await this.ScaleTo(1, 50, Easing.CubicIn);
                // Report.
                if (ButtonClicked != null)
                {
                    Debug.WriteLine("AnimatedButton. Reporting event");
                    ButtonClicked(this, EventArgs.Empty);
                }
            };
            AnimatedButtonStack.GestureRecognizers.Add(tapRecognizer);
            */
        }
    }
}

