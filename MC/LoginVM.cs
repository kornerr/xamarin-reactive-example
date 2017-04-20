using ReactiveUI;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
	public class LoginVM : ReactiveObject
	{
        public ReactiveCommand Login { get; protected set; }

        // Read-write property.
        string _username;
        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }

        // Read-write property.
        string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        // Read only property.
        readonly ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading
        {
            get { return _isLoading.Value; }
        }

        public LoginVM()
        {
            var canLogin =
                this.WhenAnyValue(
                    x => x.Username,
                    x => x.Password,
                    (us, pa) =>
                        {
                            bool IsUsernameValid = !String.IsNullOrWhiteSpace(us);
                            bool IsPasswordValid = !String.IsNullOrWhiteSpace(pa);
                            return IsUsernameValid && IsPasswordValid;
                        });
            Login =
                ReactiveCommand.Create(
                    () =>
                        {
                            // Do nothing. Just a signal.
                        },
					canLogin);
            /*
                ReactiveCommand.CreateFromTask(
                    async(arg) =>
                        {
                            // Faked loading.
                            await Task.Delay(4000).ConfigureAwait(false);
                        },
					canLogin);
                    */
            Login.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);
        }
	}
}

