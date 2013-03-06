using System.Net;
using System.Net.Http;
using UltimateTeam.Toolkit.Service;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public abstract class RequestBase
    {
        private IJsonDeserializer _jsonDeserializer;
        private static CookieContainer CookieContainer = new CookieContainer();
        protected static string SessionId;
        protected readonly HttpClient Client;
        protected static Persona _Persona;

        public IJsonDeserializer JsonDeserializer
        {
            get { return _jsonDeserializer ?? new JsonDeserializer(); }
            set { _jsonDeserializer = value; }
        }

        protected RequestBase()
        {
            var handler = new HttpClientHandler { CookieContainer = CookieContainer };
            Client = new HttpClient(handler);
        }

        public static Persona Persona
        {
            get { return _Persona; }
        }
    }
}