using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading.Tasks;

namespace MC
{
	public class GitHubClient
	{
		public GitHubClient()
		{
			Client = new RestClient("http://api.github.com");
		}

        public async Task<GitHubResourcesModel> GetResourcesAsync()
        {
            var request = new RestRequest("", Method.GET);
            var result = await Client.Execute<GitHubResourcesModel>(request);
			return result.Data;
        }

		protected IRestClient Client { get; set; }
	}

    public class GitHubResourcesModel
    {
        //[DeserializeAs(Name = "current_user_url")]
        //public string CurrentUserURL { get; set; }
        public string current_user_url { get; set; }
        //[DeserializeAs(Name = "hub_url")]
        //public string HubURL { get; set; }
        public string hub_url { get; set; }
    }
}
