using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class Currency
    {
        [DataMember(Name = "finalFunds")]
        public uint FinalFunds { get; set; }

        [DataMember(Name = "funds")]
        public uint Funds { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}