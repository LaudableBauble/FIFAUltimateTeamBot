using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class ItemDataResponse
    {
        [DataMember(Name = "itemData")]
        public List<ItemData> ItemData { get; set; }
    }
}