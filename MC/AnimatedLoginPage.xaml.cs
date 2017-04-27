using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
	public partial class AnimatedLoginPage : ContentPage
	{
		public AnimatedLoginPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Show LoginView upon appearing.
			LoginView.LayoutTo(Target.Bounds, 250, Easing.CubicIn);
        }
	}
}
