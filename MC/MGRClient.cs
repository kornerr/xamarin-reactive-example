
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading.Tasks;

namespace MC
{
    public class MGRClient
    {
        public MGRClient()
        {
            _client = new RestClient("http://absence.dev.mstatic.ru/api/v1");
        }

        public async Task<AuthModel> GetAuthAsync(string username, string password)
        {
			var request = new RestRequest("auth/token", Method.POST);
            request.AddBody(
                new {
                    login = username,
                    password = password
                });
            var result = await _client.Execute<AuthModel>(request);
            return result.Data;
        }

        protected IRestClient _client;
    }
}

