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

            // Observe animated button press.
            Debug.WriteLine("AnimatedLoginPage. AnimatedButton: '{0}'", AnimatedButton);
			var obs = 
				Observable.FromEventPattern(
					ev => AnimatedButton.ButtonClicked += ev,
					ev => AnimatedButton.ButtonClicked -= ev);
			obs.Subscribe(_ => 
            {
                Debug.WriteLine("AnimatedLoginPage. AnimatedButton clicked");
				SlideView.LayoutTo(Target.Bounds, 250, Easing.CubicIn);
            });
		}
	}
}
