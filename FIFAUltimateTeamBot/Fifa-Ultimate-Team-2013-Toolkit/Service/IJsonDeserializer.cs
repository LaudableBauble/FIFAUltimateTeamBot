using System.IO;

namespace UltimateTeam.Toolkit.Service
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(Stream stream);
    }
}