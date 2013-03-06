using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class DateOfBirth
    {
        [DataMember]
        public byte Day { get; set; }

        [DataMember]
        public byte Month { get; set; }

        [DataMember]
        public ushort Year { get; set; }
    }
}