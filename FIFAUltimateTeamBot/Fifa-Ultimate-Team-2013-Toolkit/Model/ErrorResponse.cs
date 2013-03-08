using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "debug")]
        public string Debug { get; set; }

        [DataMember(Name = "string")]
        public string String { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }
    }
}