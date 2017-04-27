using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace MC
{
	public partial class AnimatedButton : ContentView
	{
        public const string ButtonClickedCommandPropertyName = "ButtonClickedCommand";
        public static BindableProperty ButtonClickedCommandProperty =
            BindableProperty.Create(
                "ButtonClickedCommand",
                typeof(ICommand),
                typeof(AnimatedButton),
                null);

        public ICommand ButtonClickedCommand
        {
            get { return (ICommand)GetValue(ButtonClickedCommandProperty); }
            set { SetValue(ButtonClickedCommandProperty, value); }
        }

		public AnimatedButton()
		{
			InitializeComponent();

            setupTap();
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            RemoveBinding(ButtonClickedCommandProperty);
            SetBinding(
                ButtonClickedCommandProperty,
                new Binding(ButtonClickedCommandPropertyName));
        }

        private void setupTap()
        {
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += async (s, e) =>
            {
                await this.ScaleTo(0.95, 50, Easing.CubicOut);
                await this.ScaleTo(1, 50, Easing.CubicIn);
				Debug.WriteLine("AnimatedButton. Finished animating");
                // Report.
                var command = ButtonClickedCommand;
                if (command != null) {
					Debug.WriteLine("AnimatedButton. Executing command");
					command.Execute(null);
                }
            };
            this.GestureRecognizers.Add(tapRecognizer);
        }
	}
}
