using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class UserAccountInfo
    {
        [DataMember(Name = "personas")]
        public List<Persona> Personas { get; set; }
    }
}