using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
    public class GitHubResources : ReactiveObject
    {
        // RefreshStatus.
        ModelRequestStatus _refreshStatus;
        public ModelRequestStatus RefreshStatus
        {
            get { return _refreshStatus; }
            protected set { this.RaiseAndSetIfChanged(ref _refreshStatus, value); }
        }

        // GHRModel.
        GitHubResourcesModel _ghrModel;
        public GitHubResourcesModel GHRModel
        {
            get { return _ghrModel; }
            protected set { this.RaiseAndSetIfChanged(ref _ghrModel, value); }
        }

        public GitHubResources(GitHubClient client)
        {
            Client = client;
            _ghrModel = new GitHubResourcesModel();
            _refreshStatus = ModelRequestStatus.None;
        }

        public async void refresh()
        {
            Debug.WriteLine("GitHubResources. refresh");
            RefreshStatus = ModelRequestStatus.Process;
            GHRModel = await Client.GetResourcesAsync();
            RefreshStatus = ModelRequestStatus.Success;
            Debug.WriteLine(
                "GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                GHRModel.current_user_url,
                GHRModel.hub_url);
        }

        protected GitHubClient Client { get; set; }

    }
}

