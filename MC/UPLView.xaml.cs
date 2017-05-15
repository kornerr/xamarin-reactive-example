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
            Username.TextChanged += (s, e) =>
            {
                Debug.WriteLine("UPLView. Username change");
                // Report.
                if (UsernameChanged != null)
                {
                    Debug.WriteLine("UPLView. Username change reported");
                    UsernameChanged(this, EventArgs.Empty);
                }
            };
        }
    }
}

