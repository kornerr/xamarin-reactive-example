
using Newtonsoft.Json;

namespace MC
{
    public static class SerializationExtensions
    {
        public static string toJSON(this object obj)
        {
			return JsonConvert.SerializeObject(obj);
        }
    }
}

