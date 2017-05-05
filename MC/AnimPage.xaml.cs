using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace MC
{
    public partial class AnimPage : ContentPage
    {
        public AnimPage()
        {
            InitializeComponent();

            setupTap();
        }
        private void setupTap()
        {
            Debug.WriteLine("AnimPage. setupTap");
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += (s, e) =>
            {
                Debug.WriteLine("AnimPage. Tapped");
            };
			AnimatedButton.GestureRecognizers.Add(tapRecognizer);
        }
    }
}
