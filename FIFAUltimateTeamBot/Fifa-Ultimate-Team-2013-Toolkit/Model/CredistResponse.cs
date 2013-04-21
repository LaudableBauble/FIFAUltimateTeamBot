using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class CreditsResponse
    {
        [DataMember(Name = "credits")]
        public uint Credits { get; set; }

        [DataMember(Name = "currencies")]
        public List<Currency> Currencies { get; set; }

        [DataMember(Name = "unopenedPacks")]
        public UnopenedPacks UnopenedPacks { get; set; }
    }
}