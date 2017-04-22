
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
}

