using ReactiveUI;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class App : Application
    {
        public LoginVM loginVM;
        public LoginPage loginPage;

        public App()
        {
            InitializeComponent();

            loginVM = new LoginVM();
            loginPage = new LoginPage(loginVM);

            MainPage = loginPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
