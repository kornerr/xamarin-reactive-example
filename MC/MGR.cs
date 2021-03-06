using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
    public class MGR : ReactiveObject
    {
        // AuthStatus.
        ModelRequestStatus _authStatus;
        public ModelRequestStatus AuthStatus
        {
            get { return _authStatus; }
            protected set { this.RaiseAndSetIfChanged(ref _authStatus, value); }
        }

        // Auth.
        AuthModel _auth;
        public AuthModel Auth
        {
            get { return _auth; }
            protected set { this.RaiseAndSetIfChanged(ref _auth, value); }
        }

        public MGR(MGRClient client)
        {
            _client = client;
            _auth = new AuthModel();
            _authStatus = ModelRequestStatus.None;
        }

        public async void authorize(string username, string password)
        {
            Debug.WriteLine("MGR. authorize");
            AuthStatus = ModelRequestStatus.Process;
            try
            {
                Auth = await _client.GetAuthAsync(username, password);
                AuthStatus = ModelRequestStatus.Success;
                Debug.WriteLine("MGR. authorize OK: '{0}'", Auth.toJSON());
            }
            catch (Exception e)
            {
                Debug.WriteLine("MGR. authorize ERROR: '{0}'", e.Message);
                AuthStatus = ModelRequestStatus.Failure;
            }
        }

        private MGRClient _client;
    }
}

