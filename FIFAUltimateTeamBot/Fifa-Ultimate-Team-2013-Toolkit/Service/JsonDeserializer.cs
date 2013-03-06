using System.IO;
using System.Runtime.Serialization.Json;

namespace UltimateTeam.Toolkit.Service
{
    internal class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            return (T)serializer.ReadObject(stream);
        }
    }
}