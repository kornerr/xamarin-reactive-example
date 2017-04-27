using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;

using Xamarin.Forms;

namespace MC
{
	public partial class AnimationPage : ContentPage
	{
		public AnimationPage()
		{
			InitializeComponent();

            // Observe animated button press.
            Debug.WriteLine("AnimationPage. AnimatedButton: '{0}'", AnimatedButton);
			var obs = 
				Observable.FromEventPattern(
					ev => AnimatedButton.ButtonClicked += ev,
					ev => AnimatedButton.ButtonClicked -= ev);
			obs.Subscribe(_ => 
            {
                Debug.WriteLine("AnimationPage. AnimatedButton clicked");
				Source.LayoutTo(Target.Bounds, 250, Easing.CubicIn);
            });


		}
	}
}
