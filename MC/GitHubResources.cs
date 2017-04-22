using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
    public class GitHubResources : ReactiveObject
    {
        //public ReactiveCommand Login { get; protected set; }

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
            /*
            Login =
                ReactiveCommand.CreateFromTask(
                    async(arg) =>
                        {
                            Debug.WriteLine("GitHubResources. Make request");
                            _ghrModel = await Client.GetResourcesAsync();
                            Debug.WriteLine(
                                "GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                                GHRModel.current_user_url,
                                GHRModel.hub_url);
                        });
                        */
        }

        public async void refresh()
        {
            Debug.WriteLine("GitHubResources. refresh");
            GHRModel = await Client.GetResourcesAsync();
            Debug.WriteLine(
                "GitHubResources(current_user_url: '{0}' hub_url: '{1}')",
                GHRModel.current_user_url,
                GHRModel.hub_url);
        }

        protected GitHubClient Client { get; set; }

    }
}

