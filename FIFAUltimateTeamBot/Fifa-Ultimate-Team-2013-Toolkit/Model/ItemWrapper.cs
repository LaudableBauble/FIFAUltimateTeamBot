using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class ItemWrapper
    {
        [DataMember]
        public Item Item { get; set; }
    }
}