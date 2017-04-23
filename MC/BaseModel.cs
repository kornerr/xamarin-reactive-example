
namespace MC
{
    public class BaseModel
    {
        public string code { get; set; }
    }

    public class BaseModel<T> : BaseModel
    {
        public T data { get; set; }
    }
}

