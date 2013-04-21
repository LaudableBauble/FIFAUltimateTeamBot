using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class UnopenedPacks
    {
        [DataMember(Name = "preOrderedPacks")]
        public ushort PreOrderPacks { get; set; }

        [DataMember(Name = "recoveredPacks")]
        public ushort RecoveredPacks { get; set; }
    }
}