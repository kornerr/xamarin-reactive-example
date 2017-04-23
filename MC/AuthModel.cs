
using Newtonsoft.Json;

namespace MC
{
    public class AuthModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }

    public static class ObjectExtensions
    {
        public static string toJSON(this object obj)
        {
			return JsonConvert.SerializeObject(obj);
        }
    }
}

