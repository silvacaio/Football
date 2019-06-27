using Newtonsoft.Json;

namespace Football.Domain.Core.Deserialize
{
    public interface IDeserializeToObject<T>
    {
        T Deserialize(string value);
    }

    public class DeserializeToObject<T> : IDeserializeToObject<T>
    {
        public T Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
