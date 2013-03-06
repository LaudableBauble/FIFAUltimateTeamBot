using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class AuthenticationResponse
    {
        [DataMember(Name = "ipPort")]
        public string IpPort { get; set; }

        [DataMember(Name = "lastOnlineTime")]
        public string LastOnlineTime { get; set; }

        [DataMember(Name = "protocol")]
        public string Protocol { get; set; }

        [DataMember(Name = "serverTime")]
        public string ServerTime { get; set; }

        [DataMember(Name = "sid")]
        public string SessionId { get; set; }
    }
}