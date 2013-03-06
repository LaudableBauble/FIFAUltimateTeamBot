using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class ItemData
    {
        [DataMember(Name = "attributeList")]
        public List<Attribute> AttributeList { get; set; }

        [DataMember(Name = "cardSubTypeId")]
        public uint CardSubTypeId { get; set; }

        [DataMember(Name = "contract")]
        public uint Contract { get; set; }

        [DataMember(Name = "discardValue")]
        public uint? DiscardValue { get; set; }

        [DataMember(Name = "fitness")]
        public uint Fitness { get; set; }

        [DataMember(Name = "formation")]
        public string Formation { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "injuryGames")]
        public uint InjuryGames { get; set; }

        [DataMember(Name = "injuryType")]
        public string InjuryType { get; set; }

        [DataMember(Name = "itemState")]
        public string ItemState { get; set; }

        [DataMember(Name = "lastSalePrice")]
        public uint LastSalePrice { get; set; }

        [DataMember(Name = "lifeTimeStats")]
        public List<Attribute> LifeTimeStats { get; set; }

        [DataMember(Name = "morale")]
        public uint Morale { get; set; }

        [DataMember(Name = "owners")]
        public uint Owners { get; set; }

        [DataMember(Name = "preferredPosition")]
        public string PreferredPosition { get; set; }

        [DataMember(Name = "rareFlag")]
        public uint RareFlag { get; set; }

        [DataMember(Name = "rating")]
        public uint Rating { get; set; }

        [DataMember(Name = "resourceId")]
        public long ResourceId { get; set; }

        [DataMember(Name = "statsList")]
        public List<Attribute> StatsList { get; set; }

        [DataMember(Name = "suspension")]
        public uint Suspension { get; set; }

        [DataMember(Name = "teamId")]
        public uint TeamId { get; set; }

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "training")]
        public uint Training { get; set; }
    }
}