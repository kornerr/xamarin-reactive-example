using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MC
{
	public partial class AnimatedButton : ContentView
	{
		public AnimatedButton()
		{
			InitializeComponent();

            setupTap();
		}

        private void setupTap()
        {
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += async (s, e) =>
            {
                await this.ScaleTo(0.95, 50, Easing.CubicOut);
                await this.ScaleTo(1, 50, Easing.CubicIn);
            };
            this.GestureRecognizers.Add(tapRecognizer);
        }
	}
}
