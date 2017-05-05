using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace MC
{
    public partial class AnimatedButton : ContentView
    {
        public event EventHandler ButtonClicked;

        public AnimatedButton()
        {
            InitializeComponent();

            setupTap();
        }

        private void setupTap()
        {
            Debug.WriteLine("AnimatedButton. setupTap");
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += async (s, e) =>
            {
                Debug.WriteLine("AnimatedButton. Start animating");
                await this.ScaleTo(0.95, 50, Easing.CubicOut);
                await this.ScaleTo(1, 50, Easing.CubicIn);
                Debug.WriteLine("AnimatedButton. Finished animating");
                // Report.
                if (ButtonClicked != null)
                {
                    Debug.WriteLine("AnimatedButton. Reporting event");
                    ButtonClicked(this, EventArgs.Empty);
                }
            };
			AnimatedButtonStack.GestureRecognizers.Add(tapRecognizer);
        }
    }
}
